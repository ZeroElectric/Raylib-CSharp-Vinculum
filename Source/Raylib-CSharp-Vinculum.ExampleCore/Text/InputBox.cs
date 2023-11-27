
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
*   raylib [text] example - Input Box
*
*   This example has been created using raylib 3.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2017 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class InputBox
{

	const int MAX_INPUT_CHARS = 9;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [text] example - input box");

		//char name[MAX_INPUT_CHARS + 1] = "\0";      // NOTE: One extra space required for null terminator char '\0'
		string name = "";
		//int letterCount = 0;

		Rectangle textBox = new Rectangle(screenWidth / 2.0f - 100, 180, 225, 50);
		bool mouseOnText = false;

		int framesCounter = 0;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			if (CheckCollisionPointRec(GetMousePosition(), textBox)) mouseOnText = true;
			else mouseOnText = false;

			if (mouseOnText)
			{
				// Set the window's cursor to the I-Beam
				SetMouseCursor(MOUSE_CURSOR_IBEAM);

				// Get char pressed (unicode character) on the queue
				int key = GetCharPressed();

				// Check if more characters have been pressed on the same frame
				while (key > 0)
				{
					// NOTE: Only allow keys in range [32..125]
					if ((key >= 32) && (key <= 125) && (name.Length < MAX_INPUT_CHARS))
					{
						name += (char)key;
					}

					key = GetCharPressed();  // Check next character in the queue
				}

				if (IsKeyPressed(KEY_BACKSPACE))
				{
					name = name.Substring(0, name.Length - 1);
				}
			}
			else SetMouseCursor(MOUSE_CURSOR_DEFAULT);

			if (mouseOnText) framesCounter++;
			else framesCounter = 0;

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("PLACE MOUSE OVER INPUT BOX!", 240, 140, 20, GRAY);

			DrawRectangleRec(textBox, LIGHTGRAY);
			if (mouseOnText) DrawRectangleLines((int)textBox.X, (int)textBox.Y, (int)textBox.width, (int)textBox.height, RED);
			else DrawRectangleLines((int)textBox.X, (int)textBox.Y, (int)textBox.width, (int)textBox.height, DARKGRAY);

			DrawText(name, (int)textBox.X + 5, (int)textBox.Y + 8, 40, MAROON);

			DrawText(TextFormat("INPUT CHARS: %i/%i", name.Length, MAX_INPUT_CHARS), 315, 250, 20, DARKGRAY);

			if (mouseOnText)
			{
				if (name.Length < MAX_INPUT_CHARS)
				{
					// Draw blinking underscore char
					if (((framesCounter / 20) % 2) == 0) DrawText("_", (int)textBox.X + 8 + MeasureText(name, 40), (int)textBox.Y + 12, 40, MAROON);
				}
				else DrawText("Press BACKSPACE to delete chars...", 230, 300, 20, GRAY);
			}

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}
}
