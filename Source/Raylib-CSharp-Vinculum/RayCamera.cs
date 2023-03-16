
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 4.5.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

/*******************************************************************************************
*
*   rcamera - Basic camera system with support for multiple camera modes
*
*   LICENSE: zlib/libpng
*
*   Copyright (c) 2022-2023 Christoph Wagner (@Crydsch) & Ramon Santamaria (@raysan5)
*
*   This software is provided "as-is", without any express or implied warranty. In no event
*   will the authors be held liable for any damages arising from the use of this software.
*
*   Permission is granted to anyone to use this software for any purpose, including commercial
*   applications, and to alter it and redistribute it freely, subject to the following restrictions:
*
*     1. The origin of this software must not be misrepresented; you must not claim that you
*     wrote the original software. If you use this software in a product, an acknowledgment
*     in the product documentation would be appreciated but is not required.
*
*     2. Altered source versions must be plainly marked as such, and must not be misrepresented
*     as being the original software.
*
*     3. This notice may not be removed or altered from any source distribution.
*
**********************************************************************************************/

using System.Numerics;

namespace ZeroElectric.Vinculum;

public static unsafe partial class RayCamera
{
	/// <summary>
	/// Returns the cameras forward vector (normalized)
	/// </summary>
	static Vector3 GetCameraForward(Camera3D* Camera3D)
	{
		return RayMath.Vector3Normalize(RayMath.Vector3Subtract(Camera3D->target, Camera3D->position));
	}

	/// <summary>
	/// Returns the Camera3Ds up vector (normalized)
	/// Note: The up vector might not be perpendicular to the forward vector
	/// </summary>
	static Vector3 GetCameraUp(Camera3D* Camera3D)
	{
		return RayMath.Vector3Normalize(Camera3D->up);
	}

	/// <summary>
	/// Returns the Camera3Ds right vector (normalized)
	/// </summary>
	static Vector3 GetCameraRight(Camera3D* Camera3D)
	{
		Vector3 forward = GetCameraForward(Camera3D);
		Vector3 up = GetCameraUp(Camera3D);

		return RayMath.Vector3CrossProduct(forward, up);
	}

	/// <summary>
	/// Moves the Camera3D in its forward direction
	/// </summary>
	static void CameraMoveForward(Camera3D* Camera3D, float distance, bool moveInWorldPlane)
	{
		Vector3 forward = GetCameraForward(Camera3D);

		if (moveInWorldPlane)
		{
			// Project vector onto world plane
			forward.Y = 0;
			forward = RayMath.Vector3Normalize(forward);
		}

		// Scale by distance
		forward = RayMath.Vector3Scale(forward, distance);

		// Move position and target
		Camera3D->position = RayMath.Vector3Add(Camera3D->position, forward);
		Camera3D->target = RayMath.Vector3Add(Camera3D->target, forward);
	}

	/// <summary>
	/// Moves the Camera3D in its up direction
	/// </summary>
	static void CameraMoveUp(Camera3D* Camera3D, float distance)
	{
		Vector3 up = GetCameraUp(Camera3D);

		// Scale by distance
		up = RayMath.Vector3Scale(up, distance);

		// Move position and target
		Camera3D->position = RayMath.Vector3Add(Camera3D->position, up);
		Camera3D->target = RayMath.Vector3Add(Camera3D->target, up);
	}

	/// <summary>
	/// Moves the Camera3D target in its current right direction
	/// </summary>
	static void CameraMoveRight(Camera3D* Camera3D, float distance, bool moveInWorldPlane)
	{
		Vector3 right = GetCameraRight(Camera3D);

		if (moveInWorldPlane)
		{
			// Project vector onto world plane
			right.Y = 0;
			right = RayMath.Vector3Normalize(right);
		}

		// Scale by distance
		right = RayMath.Vector3Scale(right, distance);

		// Move position and target
		Camera3D->position = RayMath.Vector3Add(Camera3D->position, right);
		Camera3D->target = RayMath.Vector3Add(Camera3D->target, right);
	}

	/// <summary>
	/// Moves the Camera3D position closer/farther to/from the Camera3D target
	/// </summary>
	static void CameraMoveToTarget(Camera3D* Camera3D, float delta)
	{
		float distance = RayMath.Vector3Distance(Camera3D->position, Camera3D->target);

		// Apply delta
		distance += delta;

		// Distance must be greater than 0
		if (distance < 0) distance = 0.001f;

		// Set new distance by moving the position along the forward vector
		Vector3 forward = GetCameraForward(Camera3D);
		Camera3D->position = RayMath.Vector3Add(Camera3D->target, RayMath.Vector3Scale(forward, -distance));
	}

	/// <summary>
	/// Rotates the Camera3D around its up vector.
	/// Yaw is "looking left and right".
	/// If rotateAroundTarget is false, the Camera3D rotates around its position.
	/// Note: angle must be provided in radians.
	/// </summary>
	static void CameraYaw(Camera3D* Camera3D, float angle, bool rotateAroundTarget)
	{
		// Rotation axis
		Vector3 up = GetCameraUp(Camera3D);

		// View vector
		Vector3 targetPosition = RayMath.Vector3Subtract(Camera3D->target, Camera3D->position);

		// Rotate view vector around up axis
		targetPosition = RayMath.Vector3RotateByAxisAngle(targetPosition, up, angle);

		if (rotateAroundTarget)
		{
			// Move position relative to target
			Camera3D->position = RayMath.Vector3Subtract(Camera3D->target, targetPosition);
		}
		else // rotate around Camera3D.position
		{
			// Move target relative to position
			Camera3D->target = RayMath.Vector3Add(Camera3D->position, targetPosition);
		}
	}

	/// <summary>
	/// Rotates the Camera3D around its right vector, pitch is "looking up and down",
	///   lockView prevents Camera3D overrotation (aka "somersaults"),
	///   rotateAroundTarget defines if rotation is around target or around its position,
	///   rotateUp rotates the up direction as well (typically only usefull in Camera3D_FREE).
	/// NOTE: angle must be provided in radians
	/// </summary>
	static void CameraPitch(Camera3D* Camera3D, float angle, bool lockView, bool rotateAroundTarget, bool rotateUp)
	{
		// Up direction
		Vector3 up = GetCameraUp(Camera3D);

		// View vector
		Vector3 targetPosition = RayMath.Vector3Subtract(Camera3D->target, Camera3D->position);

		if (lockView)
		{
			// In these Camera3D modes we clamp the Pitch angle
			// to allow only viewing straight up or down.

			// Clamp view up
			float maxAngleUp = RayMath.Vector3Angle(up, targetPosition);
			maxAngleUp -= 0.001f; // avoid numerical errors
			if (angle > maxAngleUp) angle = maxAngleUp;

			// Clamp view down
			float maxAngleDown = RayMath.Vector3Angle(RayMath.Vector3Negate(up), targetPosition);
			maxAngleDown *= -1.0f; // downwards angle is negative
			maxAngleDown += 0.001f; // avoid numerical errors
			if (angle < maxAngleDown) angle = maxAngleDown;
		}

		// Rotation axis
		Vector3 right = GetCameraRight(Camera3D);

		// Rotate view vector around right axis
		targetPosition = RayMath.Vector3RotateByAxisAngle(targetPosition, right, angle);

		if (rotateAroundTarget)
		{
			// Move position relative to target
			Camera3D->position = RayMath.Vector3Subtract(Camera3D->target, targetPosition);
		}
		else // rotate around Camera3D.position
		{
			// Move target relative to position
			Camera3D->target = RayMath.Vector3Add(Camera3D->position, targetPosition);
		}

		if (rotateUp)
		{
			// Rotate up direction around right axis
			Camera3D->up = RayMath.Vector3RotateByAxisAngle(Camera3D->up, right, angle);
		}
	}

	/// <summary>
	/// Rotates the Camera3D around its forward vector,
	/// Roll is "turning your head sideways to the left or right".
	/// Note: angle must be provided in radians
	/// </summary>
	static void CameraRoll(Camera3D* Camera3D, float angle)
	{
		// Rotation axis
		Vector3 forward = GetCameraForward(Camera3D);

		// Rotate up direction around forward axis
		Camera3D->up = RayMath.Vector3RotateByAxisAngle(Camera3D->up, forward, angle);
	}

	/// <summary>
	/// Returns the Camera3D view matrix
	/// </summary>
	static Matrix4x4 GetCameraViewMatrix(Camera3D* Camera3D)
	{
		return RayMath.MatrixLookAt(Camera3D->position, Camera3D->target, Camera3D->up);
	}

	/// <summary>
	/// Returns the Camera3D projection matrix
	/// </summary>
	static Matrix4x4 GetCameraProjectionMatrix(Camera3D* Camera3D, float aspect)
	{
		if (Camera3D->projection == (int)CameraProjection.CAMERA_PERSPECTIVE)
		{
			return RayMath.MatrixPerspective(Camera3D->fovy * (MathF.PI / 180.0f), aspect, RlGl.RL_CULL_DISTANCE_NEAR, RlGl.RL_CULL_DISTANCE_FAR);
		}
		else if (Camera3D->projection == (int)CameraProjection.CAMERA_ORTHOGRAPHIC)
		{
			double top = Camera3D->fovy / 2.0;
			double right = top * aspect;

			return RayMath.MatrixOrtho(-right, right, -top, top, RlGl.RL_CULL_DISTANCE_NEAR, RlGl.RL_CULL_DISTANCE_FAR);
		}

		return RayMath.MatrixIdentity();
	}
}
