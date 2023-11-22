
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

using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum.Extensions;

public unsafe static class Helpers
{
	/// <summary>
	/// help convert a utf8 vector to string
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="utf8Text"></param>
	/// <returns></returns>
	public static string Utf8ToString<T>(T* utf8Text) where T : unmanaged
	{
		return Marshal.PtrToStringUTF8((IntPtr)utf8Text) ?? "";
	}

}
