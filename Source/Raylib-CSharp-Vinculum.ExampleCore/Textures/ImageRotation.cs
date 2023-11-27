
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - Image Rotation
*
*   Example originally created with raylib 1.0, last time updated with raylib 1.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2014-2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static class ImageRotation
{

	static int NUM_TEXTURES = 3;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - texture rotation");

		// NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
		Image image45 = LoadImage("resources/raylib_logo.png");
		Image image90 = LoadImage("resources/raylib_logo.png");
		Image imageNeg90 = LoadImage("resources/raylib_logo.png");

		unsafe
		{
			ImageRotate(&image45, 45);
			ImageRotate(&image90, 90);
			ImageRotate(&imageNeg90, -90);
		}

		Texture2D[] textures = new Texture2D[NUM_TEXTURES];

		textures[0] = LoadTextureFromImage(image45);
		textures[1] = LoadTextureFromImage(image90);
		textures[2] = LoadTextureFromImage(imageNeg90);

		int currentTexture = 0;

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT) || IsKeyPressed(KEY_RIGHT))
			{
				currentTexture = (currentTexture + 1) % NUM_TEXTURES; // Cycle between the textures
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawTexture(textures[currentTexture], (screenWidth / 2) - (textures[currentTexture].width / 2), (screenHeight / 2) - (textures[currentTexture].height / 2), WHITE);

			DrawText("Press LEFT MOUSE BUTTON to rotate the image clockwise", 250, 420, 10, DARKGRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		for (int i = 0; i < NUM_TEXTURES; i++)
		{
			UnloadTexture(textures[i]);
		}

		CloseWindow();                // Close window and OpenGL context

		return 0;
	}
}
