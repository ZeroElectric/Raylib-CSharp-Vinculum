
////------------------------------------------------------------------------------
////
//// Copyright 2022-2023 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
//// This file is licensed to you under the MPL-2.0.
//// See the LICENSE file in the project's root for more info.
////
//// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
//// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
//// Find raylib here: https://github.com/raysan5/raylib
////
////------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Models;

//********************************************************************************************/
//*
//* raylib[models] example - Load 3d model with animations and play them
//*
//* This example has been created using raylib 2.5 (www.raylib.com)
//* raylib is licensed under an unmodified zlib/libpng license(View raylib.h for details)
//*
//* Example contributed by Culacant(@culacant) and reviewed by Ramon Santamaria(@raysan5)
//*
//* Copyright(c) 2019 Culacant(@culacant) and Ramon Santamaria(@raysan5)
//*
//********************************************************************************************
//*
//* To export a model from blender, make sure it is not posed, the vertices need to be in the
//* same position as they would be in edit mode.
//* and that the scale of your models is set to 0. Scaling can be done from the export menu.
//*
//********************************************************************************************/

public unsafe static class Animation
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - model animation");

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(10.0f, 15.0f, 10.0f);         // Camera position
		camera.target = new(0.0f, 5f, 0.0f);                // Camera looking at point
		camera.up = new(0.0f, 1.0f, 0.0f);                  // Camera up vector (rotation towards target)
		camera.fovy = 50.0f;                                // Camera field-of-view Y
		camera.projection_ = CAMERA_PERSPECTIVE;            // Camera mode type

		Model model = LoadModel("resources/models/iqm/guy.iqm");                    // Load the animated model mesh and basic data
		Texture2D texture = LoadTexture("resources/models/iqm/guytex.png");         // Load model texture and set material
		SetMaterialTexture(&model.materials[0], MATERIAL_MAP_DIFFUSE, texture);     // Set model material map texture

		// Set model position
		Vector3 position = new(0.0f, 0.0f, 0.0f);

		// Load animation data
		uint animsCount = 0;
		ModelAnimation[] anims = LoadModelAnimations("resources/models/iqm/guyanim.iqm");
		int animFrameCounter = 0;

		// Limit cursor to relative movement inside the window
		DisableCursor();

		// Set our game to run at 60 frames-per-second
		SetTargetFPS(60);

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// Update camera
			UpdateCamera(ref camera, CAMERA_ORBITAL);

			// Play animation when spacebar is held down
			if (IsKeyDown(KEY_SPACE))
			{
				animFrameCounter++;
				UpdateModelAnimation(model, anims[0], animFrameCounter);
				if (animFrameCounter >= anims[0].frameCount) animFrameCounter = 0;
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);

			DrawModelEx(model, position, new(1.0f, 0.0f, 0.0f), -90.0f, new(1.0f, 1.0f, 1.0f), WHITE);

			for (int i = 0; i < model.boneCount; i++)
			{
				DrawCube(anims[0].framePoses[animFrameCounter][i].translation, 0.2f, 0.2f, 0.2f, RED);
			}

			// Draw a grid
			DrawGrid(10, 1.0f);

			EndMode3D();

			DrawText("PRESS SPACE to PLAY MODEL ANIMATION", 10, 10, 20, MAROON);
			DrawText("(c) Guy IQM 3D model by @culacant", screenWidth - 200, screenHeight - 20, 10, GRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		// Unload texture
		UnloadTexture(texture);

		// Unload model animations data
		for (uint i = 0; i < animsCount; i++)
		{
			UnloadModelAnimation(anims[i]);
		}

		// Unload model
		UnloadModel(model);

		CloseWindow();

		//--------------------------------------------------------------------------------------

		return 0;
	}

}
