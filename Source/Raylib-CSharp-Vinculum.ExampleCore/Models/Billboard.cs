
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
//* raylib[models] example - Drawing billboards
//*
//* This example has been created using raylib 1.3 (www.raylib.com)
//* raylib is licensed under an unmodified zlib/libpng license(View raylib.h for details)
//*
//* Copyright(c) 2015 Ramon Santamaria(@raysan5)
//*
//********************************************************************************************/

public unsafe static class Billboard
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - drawing billboards");

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(5.0f, 4.0f, 5.0f);
		camera.target = new(0.0f, 2.0f, 0.0f);
		camera.up = new(0.0f, 1.0f, 0.0f);
		camera.fovy = 45.0f;
		camera.Projection = CAMERA_PERSPECTIVE;

		Texture2D bill = LoadTexture("resources/billboard.png");     // Our texture billboard
		Vector3 billPosition = new(0.0f, 2.0f, 0.0f);                 // Position where draw billboard

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			UpdateCamera(ref camera, CAMERA_ORBITAL);          // Update camera

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);

			DrawGrid(10, 1.0f);        // Draw a grid

			DrawBillboard(camera, bill, billPosition, 2.0f, WHITE);

			EndMode3D();

			DrawFPS(10, 10);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadTexture(bill);        // Unload texture

		CloseWindow();              // Close window and OpenGL context

		return 0;
	}
}
