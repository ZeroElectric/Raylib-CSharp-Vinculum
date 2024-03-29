
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

namespace ZeroElectric.Vinculum.ExampleCore.Text;

/*******************************************************************************************
*
*   raylib [text] example - Text formatting
*
*   This example has been created using raylib 1.1 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2014 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TextFormatting
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [text] example - text formatting");

		int score = 100020;
		int hiscore = 200450;
		int lives = 5;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// TODO: Update your variables here

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText(TextFormat("Score: %08i", score), 200, 80, 20, RED);

			DrawText(TextFormat("HiScore: %08i", hiscore), 200, 120, 20, GREEN);

			DrawText(TextFormat("Lives: %02i", lives), 200, 160, 40, BLUE);

			DrawText(TextFormat("Elapsed Time: %02.02f ms", GetFrameTime() * 1000), 200, 220, 20, BLACK);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}
}
