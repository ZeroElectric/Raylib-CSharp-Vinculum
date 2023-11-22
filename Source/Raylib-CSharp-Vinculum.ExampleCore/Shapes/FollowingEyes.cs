
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Shapes;

/*******************************************************************************************
*
*   raylib [shapes] example - following eyes
*
*   This example has been created using raylib 2.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013-2019 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class FollowingEyes
{

	// # include <math.h>       // Required for: MathF.Atan2()

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [shapes] example - following eyes");

		Vector2 scleraLeftPosition = new Vector2(GetScreenWidth() / 2.0f - 100.0f, GetScreenHeight() / 2.0f);
		Vector2 scleraRightPosition = new Vector2(GetScreenWidth() / 2.0f + 100.0f, GetScreenHeight() / 2.0f);
		float scleraRadius = 80;

		Vector2 irisLeftPosition = new Vector2(GetScreenWidth() / 2.0f - 100.0f, GetScreenHeight() / 2.0f);
		Vector2 irisRightPosition = new Vector2(GetScreenWidth() / 2.0f + 100.0f, GetScreenHeight() / 2.0f);
		float irisRadius = 24;

		float angle = 0.0f;
		float dx = 0.0f, dy = 0.0f, dxx = 0.0f, dyy = 0.0f;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			irisLeftPosition = GetMousePosition();
			irisRightPosition = GetMousePosition();

			// Check not inside the left eye sclera
			if (!CheckCollisionPointCircle(irisLeftPosition, scleraLeftPosition, scleraRadius - 20))
			{
				dx = irisLeftPosition.X - scleraLeftPosition.X;
				dy = irisLeftPosition.Y - scleraLeftPosition.Y;

				angle = MathF.Atan2(dy, dx);

				dxx = (scleraRadius - irisRadius) * MathF.Cos(angle);
				dyy = (scleraRadius - irisRadius) * MathF.Sin(angle);

				irisLeftPosition.X = scleraLeftPosition.X + dxx;
				irisLeftPosition.Y = scleraLeftPosition.Y + dyy;
			}

			// Check not inside the right eye sclera
			if (!CheckCollisionPointCircle(irisRightPosition, scleraRightPosition, scleraRadius - 20))
			{
				dx = irisRightPosition.X - scleraRightPosition.X;
				dy = irisRightPosition.Y - scleraRightPosition.Y;

				angle = MathF.Atan2(dy, dx);

				dxx = (scleraRadius - irisRadius) * MathF.Cos(angle);
				dyy = (scleraRadius - irisRadius) * MathF.Sin(angle);

				irisRightPosition.X = scleraRightPosition.X + dxx;
				irisRightPosition.Y = scleraRightPosition.Y + dyy;
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawCircleV(scleraLeftPosition, scleraRadius, LIGHTGRAY);
			DrawCircleV(irisLeftPosition, irisRadius, BROWN);
			DrawCircleV(irisLeftPosition, 10, BLACK);

			DrawCircleV(scleraRightPosition, scleraRadius, LIGHTGRAY);
			DrawCircleV(irisRightPosition, irisRadius, DARKGREEN);
			DrawCircleV(irisRightPosition, 10, BLACK);

			DrawFPS(10, 10);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		CloseWindow();        // Close window and OpenGL context
							  //--------------------------------------------------------------------------------------

		return 0;
	}
}
