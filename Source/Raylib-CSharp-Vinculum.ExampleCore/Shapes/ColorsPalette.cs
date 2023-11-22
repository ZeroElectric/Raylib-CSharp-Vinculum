
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
*   raylib [shapes] example - Colors palette
*
*   This example has been created using raylib 2.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2014-2019 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class ColorsPalette
{

	const int MAX_COLORS_COUNT = 21;          // Number of colors available

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [shapes] example - colors palette");

		Color[] colors = new Color[MAX_COLORS_COUNT] {
		DARKGRAY, MAROON, ORANGE, DARKGREEN, DARKBLUE, DARKPURPLE, DARKBROWN,
		GRAY, RED, GOLD, LIME, BLUE, VIOLET, BROWN, LIGHTGRAY, PINK, YELLOW,
		GREEN, SKYBLUE, PURPLE, BEIGE };

		string[] colorNames = new string[MAX_COLORS_COUNT] {
		"DARKGRAY", "MAROON", "ORANGE", "DARKGREEN", "DARKBLUE", "DARKPURPLE",
		"DARKBROWN", "GRAY", "RED", "GOLD", "LIME", "BLUE", "VIOLET", "BROWN",
		"LIGHTGRAY", "PINK", "YELLOW", "GREEN", "SKYBLUE", "PURPLE", "BEIGE" };

		Rectangle[] colorsRecs = new Rectangle[MAX_COLORS_COUNT];     // Rectangles array

		// Fills colorsRecs data (for every rectangle)
		for (int i = 0; i < MAX_COLORS_COUNT; i++)
		{
			colorsRecs[i].X = 20.0f + 100.0f * (i % 7) + 10.0f * (i % 7);
			colorsRecs[i].Y = 80.0f + 100.0f * (i / 7) + 10.0f * (i / 7);
			colorsRecs[i].width = 100.0f;
			colorsRecs[i].height = 100.0f;
		}

		int[] colorState = new int[MAX_COLORS_COUNT];           // Color state: 0-DEFAULT, 1-MOUSE_HOVER

		Vector2 mousePoint = new Vector2(0.0f, 0.0f);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			mousePoint = GetMousePosition();

			for (int i = 0; i < MAX_COLORS_COUNT; i++)
			{
				if (CheckCollisionPointRec(mousePoint, colorsRecs[i])) colorState[i] = 1;
				else colorState[i] = 0;
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("raylib colors palette", 28, 42, 20, BLACK);
			DrawText("press SPACE to see all colors", GetScreenWidth() - 180, GetScreenHeight() - 40, 10, GRAY);

			for (int i = 0; i < MAX_COLORS_COUNT; i++)    // Draw all rectangles
			{
				DrawRectangleRec(colorsRecs[i], Fade(colors[i], colorState[i] == 1 ? 0.6f : 1.0f));

				if (IsKeyDown(KEY_SPACE) || colorState[i] == 1)
				{
					DrawRectangle((int)colorsRecs[i].X, (int)(colorsRecs[i].Y + colorsRecs[i].height - 26), (int)colorsRecs[i].width, 20, BLACK);
					DrawRectangleLinesEx(colorsRecs[i], 6, Fade(BLACK, 0.3f));
					DrawText(colorNames[i], (int)(colorsRecs[i].X + colorsRecs[i].width - MeasureText(colorNames[i], 10) - 12),
						(int)(colorsRecs[i].Y + colorsRecs[i].height - 20), 10, colors[i]);
				}
			}

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		CloseWindow();                // Close window and OpenGL context
									  //--------------------------------------------------------------------------------------

		return 0;
	}
}
