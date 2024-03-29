
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

namespace ZeroElectric.Vinculum.ExampleCore.Audio;

/*******************************************************************************************
*
*   raylib [audio] example - Music playing (streaming)
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class MusicPlayingStreaming
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [audio] example - music playing (streaming)");

		InitAudioDevice();              // Initialize audio device

		Music music = LoadMusicStream("resources/country.mp3");

		PlayMusicStream(music);

		float timePlayed = 0.0f;
		bool pause = false;

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose()) 
		{
			// Update
			//----------------------------------------------------------------------------------

			UpdateMusicStream(music);   // Update music buffer with new stream data

			// Restart music playing (stop and play)
			if (IsKeyPressed(KEY_SPACE))
			{
				StopMusicStream(music);
				PlayMusicStream(music);
			}

			// Pause/Resume music playing
			if (IsKeyPressed(KEY_P))
			{
				pause = !pause;

				if (pause) PauseMusicStream(music);
				else ResumeMusicStream(music);
			}

			// Get timePlayed scaled to bar dimensions (400 pixels)
			timePlayed = GetMusicTimePlayed(music) / GetMusicTimeLength(music) * 400;

			if (timePlayed > 400) StopMusicStream(music);

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("MUSIC SHOULD BE PLAYING!", 255, 150, 20, LIGHTGRAY);

			DrawRectangle(200, 200, 400, 12, LIGHTGRAY);
			DrawRectangle(200, 200, (int)timePlayed, 12, MAROON);
			DrawRectangleLines(200, 200, 400, 12, GRAY);

			DrawText("PRESS SPACE TO RESTART MUSIC", 215, 250, 20, LIGHTGRAY);
			DrawText("PRESS P TO PAUSE/RESUME MUSIC", 208, 280, 20, LIGHTGRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadMusicStream(music);		// Unload music stream buffers from RAM
										
		CloseAudioDevice();				// Close audio device (music streaming is automatically stopped)
										
		CloseWindow();					// Close window and OpenGL context

		return 0;
	}
}







