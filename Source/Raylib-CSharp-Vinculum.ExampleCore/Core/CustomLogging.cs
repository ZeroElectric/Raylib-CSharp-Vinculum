
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
*   raylib [core] example - Custom logging
*
*   This example has been created using raylib 2.1 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Pablo Marcos Oltra (@pamarcos) and reviewed by Ramon Santamaria (@raysan5)
*
*   Copyright (c) 2018 Pablo Marcos Oltra (@pamarcos) and Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Runtime.CompilerServices;
using static ZeroElectric.Vinculum.TraceLogLevel;
using System.Runtime.InteropServices;
using System.Text;

namespace ZeroElectric.Vinculum.ExampleCore.Core;
/// <summary>
/// tough to implement logging.
/// solution from : https://stackoverflow.com/a/37629480
/// windows only.  
/// </summary>
public unsafe static class CustomLogging
{

	[DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
	public static extern int vsprintf(StringBuilder buffer,string format,IntPtr args);

	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int _vscprintf(string format,IntPtr ptr);


	// Custom logging funtion
	[UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
	private static void LogCustom(int msgType, sbyte* text, sbyte* args)
	{
		Console.Write(DateTime.Now);

		switch ((TraceLogLevel)msgType)
		{
			case LOG_INFO: Console.Write($"[INFO] {msgType} :"); break;
			case LOG_ERROR: Console.Write($"[ERROR] {msgType} :"); break;
			case LOG_WARNING: Console.Write($"[WARN] {msgType} :"); break;
			case LOG_DEBUG: Console.Write($"[DEBUG] {msgType} :"); break;
			default:
				Console.Write($"[???] {msgType} :"); break;
		}

		var textStr = Marshal.PtrToStringUTF8((IntPtr)text)??"";

		var sb = new StringBuilder(_vscprintf(textStr, (IntPtr)args) + 1);
		vsprintf(sb, textStr, (IntPtr)args);

		//here formattedMessage has the value your are looking for
		var formattedMessage = sb.ToString();
		Console.WriteLine(formattedMessage);
	}


	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		// First thing we do is setting our custom logger to ensure everything raylib logs
		// will use our own logger instead of its internal one
		SetTraceLogCallback(&LogCustom);

		InitWindow(screenWidth, screenHeight, "raylib [core] example - custom logging");

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			// TODO: Update your variables here
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("Check out the console output to see the custom logger in action!", 60, 200, 20, LIGHTGRAY);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		CloseWindow();        // Close window and OpenGL context
							  //--------------------------------------------------------------------------------------

		return 0;
	}
}

