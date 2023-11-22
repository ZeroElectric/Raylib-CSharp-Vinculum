
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

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - Background scrolling
*
*   This example has been created using raylib 2.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2019 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class BackgroundScrolling
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - background scrolling");

		// NOTE: Be careful, background width must be equal or bigger than screen width
		// if not, texture should be draw more than two times for scrolling effect
		Texture2D background = LoadTexture("resources/cyberpunk_street_background.png");
		Texture2D midground = LoadTexture("resources/cyberpunk_street_midground.png");
		Texture2D foreground = LoadTexture("resources/cyberpunk_street_foreground.png");

		float scrollingBack = 0.0f;
		float scrollingMid = 0.0f;
		float scrollingFore = 0.0f;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			scrollingBack -= 0.1f;
			scrollingMid -= 0.5f;
			scrollingFore -= 1.0f;

			// NOTE: Texture is scaled twice its size, so it sould be considered on scrolling
			if (scrollingBack <= -background.width * 2) scrollingBack = 0;
			if (scrollingMid <= -midground.width * 2) scrollingMid = 0;
			if (scrollingFore <= -foreground.width * 2) scrollingFore = 0;
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(GetColor(0x052c46ff));

			// Draw background image twice
			// NOTE: Texture is scaled twice its size
			DrawTextureEx(background, new Vector2(scrollingBack, 20), 0.0f, 2.0f, WHITE);
			DrawTextureEx(background, new Vector2(background.width * 2 + scrollingBack, 20), 0.0f, 2.0f, WHITE);

			// Draw midground image twice
			DrawTextureEx(midground, new Vector2(scrollingMid, 20), 0.0f, 2.0f, WHITE);
			DrawTextureEx(midground, new Vector2(midground.width * 2 + scrollingMid, 20), 0.0f, 2.0f, WHITE);

			// Draw foreground image twice
			DrawTextureEx(foreground, new Vector2(scrollingFore, 70), 0.0f, 2.0f, WHITE);
			DrawTextureEx(foreground, new Vector2(foreground.width * 2 + scrollingFore, 70), 0.0f, 2.0f, WHITE);

			DrawText("BACKGROUND SCROLLING & PARALLAX", 10, 10, 20, RED);
			DrawText("(c) Cyberpunk Street Environment by Luis Zuno (@ansimuz)", screenWidth - 330, screenHeight - 20, 10, RAYWHITE);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadTexture(background);  // Unload background texture
		UnloadTexture(midground);   // Unload midground texture
		UnloadTexture(foreground);  // Unload foreground texture

		CloseWindow();              // Close window and OpenGL context
									//--------------------------------------------------------------------------------------

		return 0;
	}
}
