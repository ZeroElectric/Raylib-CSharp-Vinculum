
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
*   raylib [shapes] example - Draw Textured Polygon
*
*   This example has been created using raylib 3.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Chris Camacho (@codifies - bedroomcoders.co.uk) and
*   reviewed by Ramon Santamaria (@raysan5)
*
*   Copyright (c) 2021 Chris Camacho (@codifies) and Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TexturedPolygon
{
	//# include "raymath.h"

	const int MAX_POINTS = 11;      // 10 points and back to the start

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - textured polygon");

		// Define texture coordinates to map our texture to poly
		Vector2[] texcoords = new Vector2[MAX_POINTS] {
		new Vector2( 0.75f, 0.0f ),
		new Vector2( 0.25f, 0.0f ),
		new Vector2( 0.0f, 0.5f ),
		new Vector2( 0.0f, 0.75f ),
		new Vector2( 0.25f, 1.0f),
		new Vector2( 0.375f, 0.875f),
		new Vector2( 0.625f, 0.875f),
		new Vector2( 0.75f, 1.0f),
		new Vector2( 1.0f, 0.75f),
		new Vector2( 1.0f, 0.5f),
		new Vector2( 0.75f, 0.0f)  // Close the poly
	};

		// Define the base poly vertices from the UV's
		// NOTE: They can be specified in any other way
		Vector2[] points = new Vector2[MAX_POINTS];
		for (int i = 0; i < MAX_POINTS; i++)
		{
			points[i].X = (texcoords[i].X - 0.5f) * 256.0f;
			points[i].Y = (texcoords[i].Y - 0.5f) * 256.0f;
		}

		// Define the vertices drawing position
		// NOTE: Initially same as points but updated every frame
		Vector2[] positions = new Vector2[MAX_POINTS];
		for (int i = 0; i < MAX_POINTS; i++) positions[i] = points[i];

		// Load texture to be mapped to poly
		Texture texture = LoadTexture("resources/cat.png");

		float angle = 0.0f;             // Rotation angle (in degrees)

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			// Update points rotation with an angle transform
			// NOTE: Base points position are not modified
			angle++;
			for (int i = 0; i < MAX_POINTS; i++) positions[i] = Vector2Rotate(points[i], angle * DEG2RAD);
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("textured polygon", 20, 20, 20, DARKGRAY);

			DrawTexturePoly(texture, new Vector2(GetScreenWidth() / 2, GetScreenHeight() / 2), positions, texcoords, MAX_POINTS, WHITE);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadTexture(texture); // Unload texture

		CloseWindow();          // Close window and OpenGL context
								//--------------------------------------------------------------------------------------

		return 0;
	}

	static void DrawTexturePoly(Texture2D texture, Vector2 center, Vector2[] points, Vector2[] texcoords, int pointCount, Color tint)
	{
		rlSetTexture(texture.id);

		// Texturing is only supported on RL_QUADS
		rlBegin(RL_QUADS);

		rlColor4ub(tint.r, tint.g, tint.b, tint.a);

		for (int i = 0; i < pointCount - 1; i++)
		{
			rlTexCoord2f(0.5f, 0.5f);
			rlVertex2f(center.X, center.Y);

			rlTexCoord2f(texcoords[i].X, texcoords[i].Y);
			rlVertex2f(points[i].X + center.X, points[i].Y + center.Y);

			rlTexCoord2f(texcoords[i + 1].X, texcoords[i + 1].Y);
			rlVertex2f(points[i + 1].X + center.X, points[i + 1].Y + center.Y);

			rlTexCoord2f(texcoords[i + 1].X, texcoords[i + 1].Y);
			rlVertex2f(points[i + 1].X + center.X, points[i + 1].Y + center.Y);
		}
		rlEnd();

		rlSetTexture(0);
	}

}
