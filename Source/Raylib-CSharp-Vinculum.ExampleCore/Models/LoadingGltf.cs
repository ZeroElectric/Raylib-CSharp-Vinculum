
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Models;

/*******************************************************************************************
*
*   raylib [models] example - loading gltf with animations
*
*   LIMITATIONS:
*     - Only supports 1 armature per file, and skips loading it if there are multiple armatures
*     - Only supports linear interpolation (default method in Blender when checked
*       "Always Sample Animations" when exporting a GLTF file)
*     - Only supports translation/rotation/scale animation channel.path,
*       weights not considered (i.e. morph targets)
*
*   Example originally created with raylib 3.7, last time updated with raylib 4.2
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2020-2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class LoadingGLTF
{
	public static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - loading gltf");

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(10.0f, 10.0f, 10.0f);         // Camera position
		camera.target = new(0.0f, 0.0f, 0.0f);              // Camera looking at point
		camera.up = new(0.0f, 1.0f, 0.0f);                  // Camera up vector (rotation towards target)
		camera.fovy = 45.0f;                                // Camera field-of-view Y
		camera.Projection = CAMERA_PERSPECTIVE;             // Camera mode type

		// Load some models
		Model model = LoadModel("resources/models/gltf/robot.glb");

		// Load gltf model animations
		int animsCount = 0;
		int animIndex = 0;
		int animCurrentFrame = 0;

		ModelAnimation[] modelAnimations = LoadModelAnimations("resources/models/gltf/robot.glb", ref animsCount);

		// Model's position
		Vector3 position = new(0.0f, 0.0f, 0.0f);

		DisableCursor();                    // Limit cursor to relative movement inside the window

		SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			UpdateCamera(ref camera, CAMERA_THIRD_PERSON);           // Update camera

			if (IsMouseButtonPressed(MOUSE_RIGHT_BUTTON)) animIndex = (animIndex + 1) % animsCount;
			else if (IsMouseButtonPressed(MOUSE_LEFT_BUTTON))
			{
				if (animIndex > 0)
				{
					animIndex = (animIndex + animsCount - 1) % animsCount;
				}
			}

			ModelAnimation animation = modelAnimations[animIndex];
			animCurrentFrame = (animCurrentFrame + 1) % animation.frameCount;
			UpdateModelAnimation(model, animation, animCurrentFrame);

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);

			DrawModel(model, position, 1.0f, WHITE);     // Draw animated model
			DrawGrid(10, 1.0f);

			EndMode3D();

			DrawText("Use the LEFT/RIGHT mouse buttons to switch animation", 10, 10, 20, GRAY);
			DrawText($"Animation: {animation.Name}", 10, GetScreenHeight() - 20, 10, DARKGRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadModel(model);         // Unload model and meshes/material

		CloseWindow();              // Close window and OpenGL context
	}
}
