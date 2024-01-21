
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

namespace ZeroElectric.Vinculum.Extensions;

public unsafe static class Helpers
{
	/// <summary>
	/// Converts a pointer to UTF-8 encoded text to a string.
	/// </summary>
	/// <typeparam name="T">The type of the pointer.</typeparam>
	/// <param name="utf8Text">A pointer to the UTF-8 encoded text.</param>
	/// <returns>The converted string.</returns>
	public static string Utf8ToString<T>(T* utf8Text) where T : unmanaged
	{
		return Marshal.PtrToStringUTF8((IntPtr)utf8Text) ?? "";
	}
}
