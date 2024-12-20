
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
*   raylib [text] example - raylib font loading and usage
*
*   NOTE: raylib is distributed with some free to use fonts (even for commercial pourposes!)
*         To view details and credits for those fonts, check raylib license file
*
*   This example has been created using raylib 1.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2017 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class FontLoadingUsage
{

	const int MAX_FONTS = 8;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [text] example - raylib fonts");

		// NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
		Font[] fonts = new Font[MAX_FONTS];

		fonts[0] = LoadFont("resources/fonts/alagard.png");
		fonts[1] = LoadFont("resources/fonts/pixelplay.png");
		fonts[2] = LoadFont("resources/fonts/mecha.png");
		fonts[3] = LoadFont("resources/fonts/setback.png");
		fonts[4] = LoadFont("resources/fonts/romulus.png");
		fonts[5] = LoadFont("resources/fonts/pixantiqua.png");
		fonts[6] = LoadFont("resources/fonts/alpha_beta.png");
		fonts[7] = LoadFont("resources/fonts/jupiter_crash.png");

		string[] messages = new string[MAX_FONTS]{ "ALAGARD FONT designed by Hewett Tsoi",
								"PIXELPLAY FONT designed by Aleksander Shevchuk",
								"MECHA FONT designed by Captain Falcon",
								"SETBACK FONT designed by Brian Kent (AEnigma)",
								"ROMULUS FONT designed by Hewett Tsoi",
								"PIXANTIQUA FONT designed by Gerhard Grossmann",
								"ALPHA_BETA FONT designed by Brian Kent (AEnigma)",
								"JUPITER_CRASH FONT designed by Brian Kent (AEnigma)" };

		int[] spacings = new int[MAX_FONTS] { 2, 4, 8, 4, 3, 4, 4, 1 };

		Vector2[] positions = new Vector2[MAX_FONTS];

		for (int i = 0; i < MAX_FONTS; i++)
		{
			positions[i].X = screenWidth / 2.0f - MeasureTextEx(fonts[i], messages[i], fonts[i].baseSize * 2.0f, (float)spacings[i]).X / 2.0f;
			positions[i].Y = 60.0f + fonts[i].baseSize + 45.0f * i;
		}

		// Small Y position corrections
		positions[3].Y += 8;
		positions[4].Y += 2;
		positions[7].Y -= 8;

		Color[] colors = new Color[MAX_FONTS] { MAROON, ORANGE, DARKGREEN, DARKBLUE, DARKPURPLE, LIME, GOLD, RED };

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

			DrawText("free fonts included with raylib", 250, 20, 20, DARKGRAY);
			DrawLine(220, 50, 590, 50, DARKGRAY);

			for (int i = 0; i < MAX_FONTS; i++)
			{
				DrawTextEx(fonts[i], messages[i], positions[i], fonts[i].baseSize * 2.0f, (float)spacings[i], colors[i]);
			}

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		// Fonts unloading
		for (int i = 0; i < MAX_FONTS; i++) UnloadFont(fonts[i]);

		CloseWindow();                 // Close window and OpenGL context

		return 0;
	}
}
