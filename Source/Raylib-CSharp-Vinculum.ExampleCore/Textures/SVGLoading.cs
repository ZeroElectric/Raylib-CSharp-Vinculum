
////------------------------------------------------------------------------------
////
//// Copyright 2022-2023 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
//// This file is licensed to you under the MPL-2.0.
//// See the LICENSE file in the project's root for more info.
////
//// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
//// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
//// Find raylib here: https://github.com/raysan5/raylib
////
////------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - SVG loading and texture creation
*
*   NOTE: Images are loaded in CPU memory (RAM); textures are loaded in GPU memory (VRAM)
*
*   Example originally created with raylib 4.2, last time updated with raylib 4.2
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2022 Dennis Meinen (@bixxy#4258 on Discord)
*
********************************************************************************************/

public static class SVGLoading
{
	public static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - svg loading");

		Image image = LoadImageSvg("resources/test.svg", 400, 350);     // Loaded in CPU memory (RAM)
		Texture2D texture = LoadTextureFromImage(image);                // Image converted to texture, GPU memory (VRAM)

		// Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM
		UnloadImage(image);

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

			DrawTexture(texture, screenWidth / 2 - texture.width / 2, screenHeight / 2 - texture.height / 2, WHITE);

			//Red border to illustrate how the SVG is centered within the specified dimensions
			DrawRectangleLines((screenWidth / 2 - texture.width / 2) - 1, (screenHeight / 2 - texture.height / 2) - 1, texture.width + 2, texture.height + 2, RED);

			DrawText("this IS a texture loaded from an SVG file!", 300, 410, 10, GRAY);

			EndDrawing();

		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadTexture(texture);     // Texture unloading

		CloseWindow();              // Close window and OpenGL context
	}
}
