
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
*   raylib [textures] example - Texture source and destination rectangles
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TextureSourceAndDestinationRectangles
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] examples - texture source and destination rectangles");

		// NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

		Texture2D scarfy = LoadTexture("resources/scarfy.png");        // Texture loading

		int frameWidth = scarfy.width / 6;
		int frameHeight = scarfy.height;

		// Source rectangle (part of the texture to use for drawing)
		Rectangle sourceRec = new Rectangle(0.0f, 0.0f, (float)frameWidth, (float)frameHeight);

		// Destination rectangle (screen rectangle where drawing part of texture)
		Rectangle destRec = new Rectangle(screenWidth / 2.0f, screenHeight / 2.0f, frameWidth * 2.0f, frameHeight * 2.0f);

		// Origin of the texture (rotation/scale point), it's relative to destination rectangle size
		Vector2 origin = new Vector2((float)frameWidth, (float)frameHeight);

		int rotation = 0;

		SetTargetFPS(60);
		//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			rotation++;
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			// NOTE: Using DrawTexturePro() we can easily rotate and scale the part of the texture we draw
			// sourceRec defines the part of the texture we use for drawing
			// destRec defines the rectangle where our texture part will fit (scaling it to fit)
			// origin defines the point of the texture used as reference for rotation and scaling
			// rotation defines the texture rotation (using origin as rotation point)
			DrawTexturePro(scarfy, sourceRec, destRec, origin, (float)rotation, WHITE);

			DrawLine((int)destRec.X, 0, (int)destRec.X, screenHeight, GRAY);
			DrawLine(0, (int)destRec.Y, screenWidth, (int)destRec.Y, GRAY);

			DrawText("(c) Scarfy sprite by Eiden Marsal", screenWidth - 200, screenHeight - 20, 10, GRAY);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadTexture(scarfy);        // Texture unloading

		CloseWindow();                // Close window and OpenGL context
									  //--------------------------------------------------------------------------------------

		return 0;
	}
}
