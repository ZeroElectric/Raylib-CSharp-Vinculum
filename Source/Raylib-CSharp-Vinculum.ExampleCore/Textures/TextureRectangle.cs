
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

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - Texture loading and drawing a part defined by a rectangle
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2014 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TextureRectangle
{

	const int MAX_FRAME_SPEED = 15;
	const int MIN_FRAME_SPEED = 1;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [texture] example - texture rectangle");

		// NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
		Texture2D scarfy = LoadTexture("resources/scarfy.png");        // Texture loading

		Vector2 position = new Vector2(350.0f, 280.0f);
		Rectangle frameRec = new Rectangle(0.0f, 0.0f, (float)scarfy.width / 6, (float)scarfy.height);
		int currentFrame = 0;

		int framesCounter = 0;
		int framesSpeed = 8;            // Number of spritesheet frames shown by second

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			framesCounter++;

			if (framesCounter >= (60 / framesSpeed))
			{
				framesCounter = 0;
				currentFrame++;

				if (currentFrame > 5) currentFrame = 0;

				frameRec.X = (float)currentFrame * (float)scarfy.width / 6;
			}

			if (IsKeyPressed(KEY_RIGHT)) framesSpeed++;
			else if (IsKeyPressed(KEY_LEFT)) framesSpeed--;

			if (framesSpeed > MAX_FRAME_SPEED) framesSpeed = MAX_FRAME_SPEED;
			else if (framesSpeed < MIN_FRAME_SPEED) framesSpeed = MIN_FRAME_SPEED;

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawTexture(scarfy, 15, 40, WHITE);
			DrawRectangleLines(15, 40, scarfy.width, scarfy.height, LIME);
			DrawRectangleLines(15 + (int)frameRec.X, 40 + (int)frameRec.Y, (int)frameRec.width, (int)frameRec.height, RED);

			DrawText("FRAME SPEED: ", 165, 210, 10, DARKGRAY);
			DrawText(TextFormat("%02i FPS", framesSpeed), 575, 210, 10, DARKGRAY);
			DrawText("PRESS RIGHT/LEFT KEYS to CHANGE SPEED!", 290, 240, 10, DARKGRAY);

			for (int i = 0; i < MAX_FRAME_SPEED; i++)
			{
				if (i < framesSpeed) DrawRectangle(250 + 21 * i, 205, 20, 20, RED);
				DrawRectangleLines(250 + 21 * i, 205, 20, 20, MAROON);
			}

			DrawTextureRec(scarfy, frameRec, position, WHITE);  // Draw part of the texture

			DrawText("(c) Scarfy sprite by Eiden Marsal", screenWidth - 200, screenHeight - 20, 10, GRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadTexture(scarfy);       // Texture unloading

		CloseWindow();                // Close window and OpenGL context

		return 0;
	}
}
