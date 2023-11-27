
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

namespace ZeroElectric.Vinculum.ExampleCore.Other;

/*******************************************************************************************
*
*   raylib [shapes] example - Vector Angle
*
*   Example originally created with raylib 1.0, last time updated with raylib 4.6
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static class VectorAngle
{
	public static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

		Vector2 v0 = new Vector2(screenWidth / 2, screenHeight / 2);
		Vector2 v1 = v0 + new Vector2(100.0f, 80.0f);
		Vector2 v2 = new Vector2(0, 0);

		float angle = 0.0f;
		int angleMode = 0;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			float startangle = 0.0f;

			if (angleMode == 0) startangle = -Vector2LineAngle(v0, v1) * RAD2DEG;
			if (angleMode == 1) startangle = 0.0f;

			v2 = GetMousePosition();

			if (IsKeyPressed(KEY_SPACE)) angleMode = angleMode == 0 ? 1 : 0;

			if (angleMode == 0 && IsMouseButtonDown(MOUSE_RIGHT_BUTTON)) v1 = GetMousePosition();

			if (angleMode == 0)
			{
				Vector2 v1Normal = Vector2.Normalize(v1 - v0);
				Vector2 v2Normal = Vector2.Normalize(v2 - v0);

				angle = Vector2Angle(v1Normal, v2Normal) * RAD2DEG;
			}
			else if (angleMode == 1)
			{
				angle = Vector2LineAngle(v0, v2) * RAD2DEG;
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			if (angleMode == 0)
			{
				DrawText("MODE 0: Angle between V1 and V2", 10, 10, 20, BLACK);
				DrawText("Right Click to Move V2", 10, 30, 20, DARKGRAY);

				DrawLineEx(v0, v1, 2.0f, BLACK);
				DrawLineEx(v0, v2, 2.0f, RED);

				DrawCircleSector(v0, 40.0f, startangle, startangle + angle, 32, GREEN);
			}
			else if (angleMode == 1)
			{
				DrawText("MODE 1: Angle formed by line V1 to V2", 10, 10, 20, BLACK);

				DrawLine(0, screenHeight / 2, screenWidth, screenHeight / 2, LIGHTGRAY);
				DrawLineEx(v0, v2, 2.0f, RED);

				DrawCircleSector(v0, 40.0f, startangle, startangle - angle, 32, GREEN);
			}

			DrawText("v0", (int)v0.X, (int)v0.Y, 10, DARKGRAY);

			if (angleMode == 0 && (v0 - v1).Y > 0.0f) DrawText("v1", (int)v1.X, (int)v1.Y - 10, 10, DARKGRAY);
			if (angleMode == 0 && (v0 - v1).Y < 0.0f) DrawText("v1", (int)v1.X, (int)v1.Y, 10, DARKGRAY);

			if (angleMode == 1) DrawText("v1", (int)v0.X + 40, (int)v0.Y, 10, DARKGRAY);

			DrawText("v2", (int)v2.X - 10, (int)v2.Y - 10, 10, DARKGRAY);

			DrawText("Press SPACE to change MODE", 460, 10, 20, DARKGRAY);
			DrawText($"ANGLE: {angle:F2}", 10, 70, 20, LIME);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context
	}
}
