
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

namespace ZeroElectric.Vinculum.ExampleCore.Shaders;

/*******************************************************************************************
*
*   raylib [shaders] example - Raymarching shapes generation
*
*   NOTE: This example requires raylib OpenGL 3.3 for shaders support and only #version 330
*         is currently supported. OpenGL ES 2.0 platforms are not supported at the moment.
*
*   This example has been created using raylib 2.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2018 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class RaymarchingShapes
{

	const int GLSL_VERSION = 330;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		int screenWidth = 800;
		int screenHeight = 450;

		SetConfigFlags(FLAG_WINDOW_RESIZABLE);
		InitWindow(screenWidth, screenHeight, "raylib [shaders] example - raymarching shapes");

		Camera camera = new();
		camera.position = new Vector3(2.5f, 2.5f, 3.0f);    // Camera position
		camera.target = new Vector3(0.0f, 0.0f, 0.7f);      // Camera looking at point
		camera.up = new Vector3(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
		camera.fovy = 65.0f;                                // Camera field-of-view Y

		// Load raymarching shader
		// NOTE: Defining 0 (NULL) for vertex shader forces usage of internal default vertex shader
		Shader shader = LoadShader(null, TextFormat("resources/shaders/glsl%i/raymarching.fs", GLSL_VERSION));

		// Get shader locations for required uniforms
		int viewEyeLoc = GetShaderLocation(shader, "viewEye");
		int viewCenterLoc = GetShaderLocation(shader, "viewCenter");
		int runTimeLoc = GetShaderLocation(shader, "runTime");
		int resolutionLoc = GetShaderLocation(shader, "resolution");

		Vector2 resolution = new((float)screenWidth, (float)screenHeight);
		SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);

		float runTime = 0.0f;

		// Limit cursor to relative movement inside the window
		DisableCursor();

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// Update camera
			UpdateCamera(ref camera, CAMERA_FREE);

			Vector3 cameraPos = new(camera.position.X, camera.position.Y, camera.position.Z);
			Vector3 cameraTarget = new(camera.target.X, camera.target.Y, camera.target.Z);

			float deltaTime = GetFrameTime();
			runTime += deltaTime;

			// Set shader required uniform values
			SetShaderValue(shader, viewEyeLoc, cameraPos, SHADER_UNIFORM_VEC3);
			SetShaderValue(shader, viewCenterLoc, cameraTarget, SHADER_UNIFORM_VEC3);
			SetShaderValue(shader, runTimeLoc, &runTime, SHADER_UNIFORM_FLOAT);

			// Check if screen is resized
			if (IsWindowResized())
			{
				screenWidth = GetScreenWidth();
				screenHeight = GetScreenHeight();
				resolution = new((float)screenWidth, (float)screenHeight);
				SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			// We only draw a white full-screen rectangle,
			// frame is generated in shader using raymarching
			BeginShaderMode(shader);
			DrawRectangle(0, 0, screenWidth, screenHeight, WHITE);
			EndShaderMode();

			DrawText("(c) Raymarching shader by Iñigo Quilez. MIT License.", screenWidth - 280, screenHeight - 20, 10, BLACK);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		// Unload shader
		UnloadShader(shader);

		// Close window and OpenGL context
		CloseWindow();

		return 0;
	}
}
