
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Models;

//*******************************************************************************************
//*
//* raylib[models] example - Waving cubes
//*
//* This example has been created using raylib 2.5 (www.raylib.com)
//* raylib is licensed under an unmodified zlib/libpng license(View raylib.h for details)
//*
//* Example contributed by Codecat(@codecat) and reviewed by Ramon Santamaria(@raysan5)
//*
//* Copyright(c) 2019 Codecat(@codecat) and Ramon Santamaria(@raysan5)
//*
//********************************************************************************************/

public unsafe static class WavingCubes
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - waving cubes");

		// Initialize the camera
		Camera3D camera = new();
		camera.position = new(30.0f, 20.0f, 30.0f);
		camera.target = new(0.0f, 0.0f, 0.0f);
		camera.up = new(0.0f, 1.0f, 0.0f);
		camera.fovy = 70.0f;
		camera.Projection = CAMERA_PERSPECTIVE;

		// Specify the amount of blocks in each direction
		const int numBlocks = 15;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			double time = GetTime();

			// Calculate time scale for cube position and size
			float scale = (2.0f + (float)Math.Sin(time)) * 0.7f;

			// Move camera around the scene
			double cameraTime = time * 0.3;
			camera.position.X = (float)Math.Cos(cameraTime) * 40.0f;
			camera.position.Z = (float)Math.Sin(cameraTime) * 40.0f;

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);

			DrawGrid(10, 5.0f);

			for (int x = 0; x < numBlocks; x++)
			{
				for (int y = 0; y < numBlocks; y++)
				{
					for (int z = 0; z < numBlocks; z++)
					{
						// Scale of the blocks depends on x/y/z positions
						float blockScale = (x + y + z) / 30.0f;

						// Scatter makes the waving effect by adding blockScale over time
						float scatter = MathF.Sin(blockScale * 20.0f + (float)(time * 4.0f));

						// Calculate the cube position
						Vector3 cubePos = new(
									(float)(x - numBlocks / 2) * (scale * 3.0f) + scatter,
									(float)(y - numBlocks / 2) * (scale * 2.0f) + scatter,
									(float)(z - numBlocks / 2) * (scale * 3.0f) + scatter
								);

						// Pick a color with a hue depending on cube position for the rainbow color effect
						Color cubeColor = ColorFromHSV((float)(((x + y + z) * 18) % 360), 0.75f, 0.9f);

						// Calculate cube size
						float cubeSize = (2.4f - scale) * blockScale;

						// And finally, draw the cube!
						DrawCube(cubePos, cubeSize, cubeSize, cubeSize, cubeColor);
					}
				}
			}

			EndMode3D();

			DrawFPS(10, 10);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}
}
