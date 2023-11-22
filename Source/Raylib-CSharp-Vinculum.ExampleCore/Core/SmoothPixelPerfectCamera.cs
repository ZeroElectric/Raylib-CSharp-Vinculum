
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

/*******************************************************************************************
*
*   raylib [core] example - smooth pixel-perfect camera
*
*   This example has been created using raylib 3.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Giancamillo Alessandroni (@NotManyIdeasDev) and
*   reviewed by Ramon Santamaria (@raysan5)
*
*   Copyright (c) 2021 Giancamillo Alessandroni (@NotManyIdeasDev) and Ramon Santamaria (@raysan5)
*
********************************************************************************************/

namespace ZeroElectric.Vinculum.ExampleCore.Core;

public unsafe static class SmoothPixelPerfectCamera
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		const int virtualScreenWidth = 160;
		const int virtualScreenHeight = 90;

		const float virtualRatio = (float)screenWidth / (float)virtualScreenWidth;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - smooth pixel-perfect camera");

		Camera2D worldSpaceCamera = new();  // Game world camera
		worldSpaceCamera.zoom = 1.0f;

		Camera2D screenSpaceCamera = new(); // Smoothing camera
		screenSpaceCamera.zoom = 1.0f;

		RenderTexture2D target = LoadRenderTexture(virtualScreenWidth, virtualScreenHeight); // This is where we'll draw all our objects.

		Rectangle rec01 = new( 70.0f, 35.0f, 20.0f, 20.0f );
		Rectangle rec02 = new( 90.0f, 55.0f, 30.0f, 10.0f );
		Rectangle rec03 = new( 80.0f, 65.0f, 15.0f, 25.0f );

		// The target's height is flipped (in the source Rectangle), due to OpenGL reasons
		Rectangle sourceRec = new( 0.0f, 0.0f, (float)target.texture.width, -(float)target.texture.height );
		Rectangle destRec =new( -virtualRatio, -virtualRatio, screenWidth + (virtualRatio * 2), screenHeight + (virtualRatio * 2) );

		Vector2 origin =new( 0.0f, 0.0f );

		float rotation = 0.0f;

		float cameraX = 0.0f;
		float cameraY = 0.0f;

		SetTargetFPS(60);
		//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			rotation += 60.0f * GetFrameTime();   // Rotate the rectangles, 60 degrees per second

			// Make the camera move to demonstrate the effect
			cameraX = (MathF.Sin((float)GetTime()) * 50.0f) - 10.0f;
			cameraY = MathF.Cos((float)GetTime()) * 30.0f;

			// Set the camera's target to the values computed above
			screenSpaceCamera.target = new( cameraX, cameraY );

			// Round worldSpace coordinates, keep decimals into screenSpace coordinates
			worldSpaceCamera.target.X = (int)screenSpaceCamera.target.X;
			screenSpaceCamera.target.X -= worldSpaceCamera.target.X;
			screenSpaceCamera.target.X *= virtualRatio;

			worldSpaceCamera.target.Y = (int)screenSpaceCamera.target.Y;
			screenSpaceCamera.target.Y -= worldSpaceCamera.target.Y;
			screenSpaceCamera.target.Y *= virtualRatio;
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginTextureMode(target);
			ClearBackground(RAYWHITE);

			BeginMode2D(worldSpaceCamera);
			DrawRectanglePro(rec01, origin, rotation, BLACK);
			DrawRectanglePro(rec02, origin, -rotation, RED);
			DrawRectanglePro(rec03, origin, rotation + 45.0f, BLUE);
			EndMode2D();
			EndTextureMode();

			BeginDrawing();
			ClearBackground(RED);

			BeginMode2D(screenSpaceCamera);
			DrawTexturePro(target.texture, sourceRec, destRec, origin, 0.0f, WHITE);
			EndMode2D();

			DrawText(TextFormat("Screen resolution: %ix%i", screenWidth, screenHeight), 10, 10, 20, DARKBLUE);
			DrawText(TextFormat("World resolution: %ix%i", virtualScreenWidth, virtualScreenHeight), 10, 40, 20, DARKGREEN);
			DrawFPS(GetScreenWidth() - 95, 10);
			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadRenderTexture(target);    // Unload render texture

		CloseWindow();                  // Close window and OpenGL context
										//--------------------------------------------------------------------------------------

		return 0;
	}
}

