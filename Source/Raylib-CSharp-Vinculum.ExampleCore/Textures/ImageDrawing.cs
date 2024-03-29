
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
*   raylib [textures] example - Image loading and drawing on it
*
*   NOTE: Images are loaded in CPU memory (RAM); textures are loaded in GPU memory (VRAM)
*
*   This example has been created using raylib 1.4 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class ImageDrawing
{
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - image drawing");

		// NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

		Image cat = LoadImage("resources/cat.png");             // Load image in CPU memory (RAM)
		ImageCrop(&cat, new Rectangle(100, 10, 280, 380));      // Crop an image piece
		ImageFlipHorizontal(&cat);                              // Flip cropped image horizontally
		ImageResize(&cat, 150, 200);                            // Resize flipped-cropped image

		Image parrots = LoadImage("resources/parrots.png");     // Load image in CPU memory (RAM)

		// Draw one image over the other with a scaling of 1.5f
		ImageDraw(&parrots, cat, new Rectangle(0, 0, (float)cat.width, (float)cat.height), new Rectangle(30, 40, cat.width * 1.5f, cat.height * 1.5f), WHITE);
		ImageCrop(&parrots, new Rectangle(0, 50, (float)parrots.width, (float)parrots.height - 100)); // Crop resulting image

		// Draw on the image with a few image draw methods
		ImageDrawPixel(&parrots, 10, 10, RAYWHITE);
		ImageDrawCircle(&parrots, 10, 10, 5, RAYWHITE);
		ImageDrawRectangle(&parrots, 5, 20, 10, 10, RAYWHITE);

		UnloadImage(cat);       // Unload image from RAM

		// Load custom font for frawing on image
		Font font = LoadFont("resources/custom_jupiter_crash.png");

		// Draw over image using custom font
		ImageDrawTextEx(ref parrots, font, "PARROTS & CAT", new Vector2(300, 230), (float)font.baseSize, -2, WHITE);

		UnloadFont(font);       // Unload custom font (already drawn used on image)

		Texture2D texture = LoadTextureFromImage(parrots);      // Image converted to texture, uploaded to GPU memory (VRAM)
		UnloadImage(parrots);   // Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM

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

			DrawTexture(texture, screenWidth / 2 - texture.width / 2, screenHeight / 2 - texture.height / 2 - 40, WHITE);
			DrawRectangleLines(screenWidth / 2 - texture.width / 2, screenHeight / 2 - texture.height / 2 - 40, texture.width, texture.height, DARKGRAY);

			DrawText("We are drawing only one texture from various images composed!", 240, 350, 10, DARKGRAY);
			DrawText("Source images have been cropped, scaled, flipped and copied one over the other.", 190, 370, 10, DARKGRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadTexture(texture);       // Texture unloading

		CloseWindow();                // Close window and OpenGL context

		return 0;
	}

}
