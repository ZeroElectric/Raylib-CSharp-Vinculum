
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

namespace ZeroElectric.Vinculum.ExampleCore.Shapes;

/*******************************************************************************************
*
*   raylib [shapes] example - Cubic-bezier lines
*
*   This example has been created using raylib 1.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2017 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class LinesCubicBezier
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		SetConfigFlags(FLAG_MSAA_4X_HINT);
		InitWindow(screenWidth, screenHeight, "raylib [shapes] example - cubic-bezier lines");

		Vector2 start = new Vector2(0, 0);
		Vector2 end = new Vector2((float)screenWidth, (float)screenHeight);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			if (IsMouseButtonDown(MOUSE_BUTTON_LEFT)) start = GetMousePosition();
			else if (IsMouseButtonDown(MOUSE_BUTTON_RIGHT)) end = GetMousePosition();

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("USE MOUSE LEFT-RIGHT CLICK to DEFINE LINE START and END POINTS", 15, 20, 20, GRAY);

			DrawLineBezier(start, end, 2.0f, RED);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}
}
