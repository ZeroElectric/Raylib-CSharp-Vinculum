
////------------------------------------------------------------------------------
////
//// Copyright 2022-2024 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
//// This file is licensed to you under the MPL-2.0.
//// See the LICENSE file in the project's root for more info.
////
//// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
//// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
//// Find raylib here: https://github.com/raysan5/raylib
////
////------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Core;

/*******************************************************************************************
*
*   raylib [core] example - VR Simulator (Oculus Rift CV1 parameters)
*
*   This example has been created using raylib 3.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2017-2021 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class VrSimulator
{

	const int GLSL_VERSION = 330;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		// NOTE: screenWidth/screenHeight should match VR device aspect ratio
		InitWindow(screenWidth, screenHeight, "raylib [core] example - vr simulator");

		// VR device parameters definition
		VrDeviceInfo device = new()
		{
			// Oculus Rift CV1 parameters for simulator
			hResolution = 2160,                 // Horizontal resolution in pixels
			vResolution = 1200,                 // Vertical resolution in pixels
			hScreenSize = 0.133793f,            // Horizontal size in meters
			vScreenSize = 0.0669f,              // Vertical size in meters
			vScreenCenter = 0.04678f,           // Screen center in meters
			eyeToScreenDistance = 0.041f,       // Distance between eye and display in meters
			lensSeparationDistance = 0.07f,     // Lens separation distance in meters
			interpupillaryDistance = 0.07f,     // IPD (distance between pupils) in meters
		};

		// NOTE: CV1 uses fresnel-hybrid-asymmetric lenses with specific compute shaders
		// Following parameters are just an approximation to CV1 distortion stereo rendering
		device.lensDistortionValues[0] = 1.0f;     // Lens distortion constant parameter 0
		device.lensDistortionValues[1] = 0.22f;    // Lens distortion constant parameter 1
		device.lensDistortionValues[2] = 0.24f;    // Lens distortion constant parameter 2
		device.lensDistortionValues[3] = 0.0f;     // Lens distortion constant parameter 3
		device.chromaAbCorrection[0] = 0.996f;     // Chromatic aberration correction parameter 0
		device.chromaAbCorrection[1] = -0.004f;    // Chromatic aberration correction parameter 1
		device.chromaAbCorrection[2] = 1.014f;     // Chromatic aberration correction parameter 2
		device.chromaAbCorrection[3] = 0.0f;       // Chromatic aberration correction parameter 3

		// Load VR stereo config for VR device parameteres (Oculus Rift CV1 parameters)
		VrStereoConfig config = LoadVrStereoConfig(device);

		// Distortion shader (uses device lens distortion and chroma)
		Shader distortion = LoadShader(null, TextFormat("resources/distortion%i.fs", GLSL_VERSION));

		// Update distortion shader with lens and distortion-scale parameters
		SetShaderValue(distortion, GetShaderLocation(distortion, "leftLensCenter"),
					   config.leftLensCenter, SHADER_UNIFORM_VEC2);
		SetShaderValue(distortion, GetShaderLocation(distortion, "rightLensCenter"),
					   config.rightLensCenter, SHADER_UNIFORM_VEC2);
		SetShaderValue(distortion, GetShaderLocation(distortion, "leftScreenCenter"),
					   config.leftScreenCenter, SHADER_UNIFORM_VEC2);
		SetShaderValue(distortion, GetShaderLocation(distortion, "rightScreenCenter"),
					   config.rightScreenCenter, SHADER_UNIFORM_VEC2);

		SetShaderValue(distortion, GetShaderLocation(distortion, "scale"),
					   config.scale, SHADER_UNIFORM_VEC2);
		SetShaderValue(distortion, GetShaderLocation(distortion, "scaleIn"),
					   config.scaleIn, SHADER_UNIFORM_VEC2);
		SetShaderValue(distortion, GetShaderLocation(distortion, "deviceWarpParam"),
					   device.lensDistortionValues, SHADER_UNIFORM_VEC4);
		SetShaderValue(distortion, GetShaderLocation(distortion, "chromaAbParam"),
					   device.chromaAbCorrection, SHADER_UNIFORM_VEC4);

		// Initialize framebuffer for stereo rendering
		// NOTE: Screen size should match HMD aspect ratio
		RenderTexture2D target = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(5.0f, 2.0f, 5.0f);    // Camera position
		camera.target = new(0.0f, 2.0f, 0.0f);      // Camera looking at point
		camera.up = new(0.0f, 1.0f, 0.0f);          // Camera up vector
		camera.fovy = 60.0f;                                // Camera field-of-view Y
		camera.Projection = CAMERA_PERSPECTIVE;             // Camera type

		Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

		// Limit cursor to relative movement inside the window
		DisableCursor();

		// Set  to run at 60 frames-per-second
		SetTargetFPS(60);

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update

			UpdateCamera(ref camera, CAMERA_FIRST_PERSON);          // Update camera

			// Draw
			//----------------------------------------------------------------------------------

			BeginTextureMode(target);
			ClearBackground(RAYWHITE);
			BeginVrStereoMode(config);
			BeginMode3D(camera);

			DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, RED);
			DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, MAROON);
			DrawGrid(40, 1.0f);

			EndMode3D();
			EndVrStereoMode();
			EndTextureMode();

			BeginDrawing();
			ClearBackground(RAYWHITE);
			BeginShaderMode(distortion);
			DrawTextureRec(target.texture, new(
				0, 0, (float)target.texture.width,
							  (float)-target.texture.height), new(0.0f, 0.0f), WHITE);
			EndShaderMode();
			DrawFPS(10, 10);
			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadVrStereoConfig(config);   // Unload stereo config

		UnloadRenderTexture(target);    // Unload stereo render fbo
		UnloadShader(distortion);       // Unload distortion shader

		CloseWindow();                  // Close window and OpenGL context

		return 0;
	}
}
