
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

namespace ZeroElectric.Vinculum.ExampleCore.Core;

/*******************************************************************************************
*
*   raylib [core] example - Windows drop files
*
*   This example only works on platforms that support drag & drop (Windows, Linux, OSX, Html5?)
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class WindowsDropFiles
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - drop files");

		int count = 0;
		//char** droppedFiles = { 0 };
		string[] droppedFiles = null;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			if (IsFileDropped())
			{
				//droppedFiles = GetDroppedFiles(&count);
				droppedFiles = GetDroppedFilesAndClear();
				count = droppedFiles.Length;
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			if (count == 0) DrawText("Drop your files to this window!", 100, 40, 20, DARKGRAY);
			else
			{
				DrawText("Dropped files:", 100, 40, 20, DARKGRAY);

				for (int i = 0; i < count; i++)
				{
					if (i % 2 == 0) DrawRectangle(0, 85 + 40 * i, screenWidth, 40, Fade(LIGHTGRAY, 0.5f));
					else DrawRectangle(0, 85 + 40 * i, screenWidth, 40, Fade(LIGHTGRAY, 0.3f));

					DrawText(droppedFiles[i], 120, 100 + 40 * i, 10, GRAY);
				}

				DrawText("Drop new files...", 100, 110 + 40 * count, 20, DARKGRAY);
			}

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();          // Close window and OpenGL context

		return 0;
	}
}
