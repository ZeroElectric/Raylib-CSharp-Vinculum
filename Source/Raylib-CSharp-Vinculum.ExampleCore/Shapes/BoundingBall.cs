
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
*   raylib [shapes] example - bouncing ball
*
*   This example has been created using raylib 1.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class BoundingBall
{

	public static int main()
	{
		// Initialization
		//---------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [shapes] example - bouncing ball");

		Vector2 ballPosition = new Vector2(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
		Vector2 ballSpeed = new Vector2(5.0f, 4.0f);
		int ballRadius = 20;

		bool pause = false;
		int framesCounter = 0;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//----------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//-----------------------------------------------------
			if (IsKeyPressed(KEY_SPACE)) pause = !pause;

			if (!pause)
			{
				ballPosition.X += ballSpeed.X;
				ballPosition.Y += ballSpeed.Y;

				// Check walls collision for bouncing
				if ((ballPosition.X >= (GetScreenWidth() - ballRadius)) || (ballPosition.X <= ballRadius)) ballSpeed.X *= -1.0f;
				if ((ballPosition.Y >= (GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius)) ballSpeed.Y *= -1.0f;
			}
			else framesCounter++;
			//-----------------------------------------------------

			// Draw
			//-----------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawCircleV(ballPosition, (float)ballRadius, MAROON);
			DrawText("PRESS SPACE to PAUSE BALL MOVEMENT", 10, GetScreenHeight() - 25, 20, LIGHTGRAY);

			// On pause, we draw a blinking message
			if (pause && ((framesCounter / 30) % 2) != 0) DrawText("PAUSED", 350, 200, 30, GRAY);

			DrawFPS(10, 10);

			EndDrawing();
			//-----------------------------------------------------
		}

		// De-Initialization
		//---------------------------------------------------------
		CloseWindow();        // Close window and OpenGL context
							  //----------------------------------------------------------

		return 0;
	}
}
