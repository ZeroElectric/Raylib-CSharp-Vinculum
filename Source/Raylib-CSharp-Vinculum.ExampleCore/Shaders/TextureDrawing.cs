
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

namespace ZeroElectric.Vinculum.ExampleCore.Shaders;

/*******************************************************************************************
*
*   raylib [textures] example - Texture drawing
*
*   This example illustrates how to draw on a blank texture using a shader
*
*   This example has been created using raylib 2.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Michał Ciesielski and reviewed by Ramon Santamaria (@raysan5)
*
*   Copyright (c) 2019 Michał Ciesielski and Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TextureDrawing
{

	const int GLSL_VERSION = 330;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [shaders] example - texture drawing");

		Image imBlank = GenImageColor(1024, 1024, BLANK);
		Texture2D texture = LoadTextureFromImage(imBlank);  // Load blank texture to fill on shader
		UnloadImage(imBlank);

		// NOTE: Using GLSL 330 shader version, on OpenGL ES 2.0 use GLSL 100 shader version
		Shader shader = LoadShader(null, TextFormat("resources/shaders/glsl%i/cubes_panning.fs", GLSL_VERSION));

		float time = 0.0f;
		int timeLoc = GetShaderLocation(shader, "uTime");
		SetShaderValue(shader, timeLoc, &time, SHADER_UNIFORM_FLOAT);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			time = (float)GetTime();
			SetShaderValue(shader, timeLoc, &time, SHADER_UNIFORM_FLOAT);

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginShaderMode(shader);    // Enable our custom shader for next shapes/textures drawings
			DrawTexture(texture, 0, 0, WHITE);  // Drawing BLANK texture, all magic happens on shader
			EndShaderMode();            // Disable our custom shader, return to default shader

			DrawText("BACKGROUND is PAINTED and ANIMATED on SHADER!", 10, 10, 20, MAROON);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadShader(shader);

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}
}
