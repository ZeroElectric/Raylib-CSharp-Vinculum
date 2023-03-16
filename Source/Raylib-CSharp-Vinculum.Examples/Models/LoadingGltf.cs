
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

namespace ZeroElectric.Vinculum.Examples.Models;

/// <summary>/*******************************************************************************************
//*
//* raylib[models] example - Load models gltf
//*
//* This example has been created using raylib 3.5 (www.raylib.com)
//* raylib is licensed under an unmodified zlib/libpng license(View raylib.h for details)
//*
//* NOTE: To export a model from Blender, make sure it is not posed, the vertices need to be
//*   in the same position as they would be in edit mode.
//*   Also make sure the scale parameter of your models is set to 0.0,
//* scaling can be applied from the export menu.
//*
//* Example contributed by Hristo Stamenov (@object71) and reviewed by Ramon Santamaria (@raysan5)
//*
//* Copyright (c) 2021 Hristo Stamenov (@object71) and Ramon Santamaria (@raysan5)
//*
//********************************************************************************************/
///</summary>
public unsafe static class LoadingGltf
{
	const int MAX_GLTF_MODELS = 8;
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - model");

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(10.0f, 10.0f, 10.0f); // Camera position
		camera.target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
		camera.up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
		camera.fovy = 45.0f;                                // Camera field-of-view Y
		camera.projection_ = CAMERA_PERSPECTIVE;             // Camera mode type

		// Load some models
		Model[] model = new Model[MAX_GLTF_MODELS];
		model[0] = LoadModel("resources/models/gltf/raylib_logo_3d.glb");
		model[1] = LoadModel("resources/models/gltf/robot.glb");

		int currentModel = 0;

		Vector3 position = new(0.0f, 0.0f, 0.0f);    // Set model position

		SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
											//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())        // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			UpdateCamera(ref camera, CameraMode.CAMERA_ORBITAL);          // Update camera

			if (IsKeyReleased(KEY_RIGHT))
			{
				currentModel++;
				if (currentModel == MAX_GLTF_MODELS) currentModel = 0;
			}

			if (IsKeyReleased(KEY_LEFT))
			{
				currentModel--;
				if (currentModel < 0) currentModel = MAX_GLTF_MODELS - 1;
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(SKYBLUE);

			BeginMode3D(camera);

			DrawModel(model[currentModel], position, 1.0f, WHITE);
			DrawGrid(10, 1.0f);         // Draw a grid

			EndMode3D();

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		for (int i = 0; i < MAX_GLTF_MODELS; i++) UnloadModel(model[i]);  // Unload models

		CloseWindow();              // Close window and OpenGL context
									//--------------------------------------------------------------------------------------

		return 0;
	}
}
