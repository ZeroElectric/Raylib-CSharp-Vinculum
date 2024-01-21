
//------------------------------------------------------------------------------
//
// Copyright 2022-2024 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum.ExampleCore.Audio;

/*******************************************************************************************
*
*   raylib [audio] example - Mixed audio processing
*
*   Example originally created with raylib 4.2, last time updated with raylib 4.2
*
*   Example contributed by hkc (@hatkidchan) and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 hkc (@hatkidchan)
*
********************************************************************************************/

public static class MixedAudioProcessing
{
	//
	// P/Invoke

	public unsafe delegate void AudioCallback(void* buffer, uint length);

	[System.Runtime.InteropServices.DllImport("raylib", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, ExactSpelling = true)]
	public static extern void AttachAudioMixedProcessor(AudioCallback AudioCallback);

	[System.Runtime.InteropServices.DllImport("raylib", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, ExactSpelling = true)]
	public static extern void DetachAudioMixedProcessor(AudioCallback AudioCallback);

	//

	static float exponent = 1.0f;                       // Audio exponentiation value
	static float[] averageVolume = new float[400];      // Average volume history

	public static unsafe int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [audio] example - processing mixed output");
		InitAudioDevice();              // Initialize audio device

		AttachAudioMixedProcessor(ProcessAudio);

		Music music = LoadMusicStream("resources/audio/country.mp3");
		Sound sound = LoadSound("resources/audio/coin.wav");

		PlayMusicStream(music);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			UpdateMusicStream(music);   // Update music buffer with new stream data

			if (IsKeyPressed(KEY_LEFT)) exponent -= 0.05f;
			if (IsKeyPressed(KEY_RIGHT)) exponent += 0.05f;

			if (exponent <= 0.5f) exponent = 0.5f;
			if (exponent >= 3.0f) exponent = 3.0f;

			if (IsKeyPressed(KEY_SPACE)) PlaySound(sound);

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("MUSIC SHOULD BE PLAYING!", 255, 150, 20, LIGHTGRAY);

			DrawText(TextFormat("EXPONENT = %.2f", exponent), 215, 180, 20, LIGHTGRAY);

			DrawRectangle(199, 199, 402, 34, LIGHTGRAY);
			for (int i = 0; i < 400; i++)
			{
				DrawLine(201 + i, 232 - (int)(averageVolume[i] * 32), 201 + i, 232, MAROON);
			}
			DrawRectangleLines(199, 199, 402, 34, GRAY);

			DrawText("PRESS SPACE TO PLAY OTHER SOUND", 200, 250, 20, LIGHTGRAY);
			DrawText("USE LEFT AND RIGHT ARROWS TO ALTER DISTORTION", 140, 280, 20, LIGHTGRAY);

			EndDrawing();

		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadMusicStream(music);   // Unload music stream buffers from RAM

		DetachAudioMixedProcessor(ProcessAudio);  // Disconnect audio processor

		CloseAudioDevice();         // Close audio device (music streaming is automatically stopped)

		CloseWindow();        // Close window and OpenGL context

		return 0;
	}

	//------------------------------------------------------------------------------------
	// Audio processing function
	//------------------------------------------------------------------------------------
	static unsafe void ProcessAudio(void* buffer, uint frames)
	{
		float[] samples = new float[frames];
		Marshal.Copy((IntPtr)buffer, samples, 0, (int)frames);

		float average = 0.0f;               // Temporary average volume

		for (uint frame = 0; frame < frames; frame++)
		{
			float left = samples[frame];
			float right = samples[frame];

			left = MathF.Pow(fabsf(left), exponent) * ((left < 0.0f) ? -1.0f : 1.0f);
			right = MathF.Pow(fabsf(right), exponent) * ((right < 0.0f) ? -1.0f : 1.0f);

			average += fabsf(left) / frames;   // accumulating average volume
			average += fabsf(right) / frames;
		}

		// Moving history to the left
		for (int i = 0; i < 399; i++)
		{
			averageVolume[i] = averageVolume[i + 1];
		}

		averageVolume[399] = average;         // Adding last average value
	}
}
