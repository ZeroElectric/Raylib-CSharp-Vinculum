
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

using static ZeroElectric.Vinculum.ExampleCore.Core.BasicScreenManager.GameScreen;

namespace ZeroElectric.Vinculum.ExampleCore.Core;

/*******************************************************************************************
*
*   raylib [core] examples - basic screen manager
*
*   This example illustrates a very simple screen manager based on a states machines
*
*   This test has been created using raylib 1.1 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2021 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static class BasicScreenManager
{	
	public enum GameScreen { LOGO = 0, TITLE, GAMEPLAY, ENDING }

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - basic screen manager");

		GameScreen currentScreen = LOGO;

		int framesCounter = 0; // Useful to count frames

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			switch (currentScreen)
			{
				case LOGO:
					{
						// TODO: Update LOGO screen variables here!

						framesCounter++; // Count frames

						// Wait for 2 seconds (120 frames) before jumping to TITLE screen
						if (framesCounter > 120)
						{
							currentScreen = TITLE;
						}
					}
					break;
				case TITLE:
					{
						// TODO: Update TITLE screen variables here!

						// Press enter to change to GAMEPLAY screen
						if (IsKeyPressed(KEY_ENTER) || IsGestureDetected((uint)GESTURE_TAP))
						{
							currentScreen = GAMEPLAY;
						}
					}
					break;
				case GAMEPLAY:
					{
						// TODO: Update GAMEPLAY screen variables here!

						// Press enter to change to ENDING screen
						if (IsKeyPressed(KEY_ENTER) || IsGestureDetected((uint)GESTURE_TAP))
						{
							currentScreen = ENDING;
						}
					}
					break;
				case ENDING:
					{
						// TODO: Update ENDING screen variables here!

						// Press enter to return to TITLE screen
						if (IsKeyPressed(KEY_ENTER) || IsGestureDetected((uint)GESTURE_TAP))
						{
							currentScreen = TITLE;
						}
					}
					break;
				default: break;
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			switch (currentScreen)
			{
				case LOGO:
					{
						// TODO: Draw LOGO screen here!
						DrawText("LOGO SCREEN", 20, 20, 40, LIGHTGRAY);
						DrawText("WAIT for 2 SECONDS...", 290, 220, 20, GRAY);

					}
					break;
				case TITLE:
					{
						// TODO: Draw TITLE screen here!
						DrawRectangle(0, 0, screenWidth, screenHeight, GREEN);
						DrawText("TITLE SCREEN", 20, 20, 40, DARKGREEN);
						DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, DARKGREEN);

					}
					break;
				case GAMEPLAY:
					{
						// TODO: Draw GAMEPLAY screen here!
						DrawRectangle(0, 0, screenWidth, screenHeight, PURPLE);
						DrawText("GAMEPLAY SCREEN", 20, 20, 40, MAROON);
						DrawText("PRESS ENTER or TAP to JUMP to ENDING SCREEN", 130, 220, 20, MAROON);

					}
					break;
				case ENDING:
					{
						// TODO: Draw ENDING screen here!
						DrawRectangle(0, 0, screenWidth, screenHeight, BLUE);
						DrawText("ENDING SCREEN", 20, 20, 40, DARKBLUE);
						DrawText("PRESS ENTER or TAP to RETURN to TITLE SCREEN", 120, 220, 20, DARKBLUE);

					}
					break;
				default: break;
			}

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow(); // Close window and OpenGL context

		return 0;
	}
}

