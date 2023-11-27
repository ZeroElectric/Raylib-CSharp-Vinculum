
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

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum.Extensions;

/// <summary>
/// This is a helper for marshalling `byte` sized CBOOL's to/from .NET's `bool` type.
/// <para>You should be able to ignore this type, treat it just as you would a normal `bool`.</para>
/// </summary>
/// <remarks>
/// See: https://docs.microsoft.com/en-us/dotnet/standard/native-interop/customize-struct-marshaling#customizing-boolean-field-marshaling for reasoning behind this type.
/// </remarks>
[DebuggerDisplay("{ToString(),raw}")]
public readonly partial struct Bool : IEquatable<Bool>
{
	/// <summary>
	/// Represents a CBOOL's boolean value.
	/// </summary>
	[MarshalAs(UnmanagedType.U1)]
	private readonly byte _val;

	/// <summary>
	/// Initializes a new instance of <see cref="Bool"/> with the specified boolean value.
	/// </summary>
	/// <param name="value">The boolean value to initialize the <see cref="Bool"/> with.</param>
	public Bool(bool value)
	{
		_val = Convert.ToByte(value);
	}

	/// <summary>
	/// Initializes a new instance of <see cref="Bool"/> with the specified byte value.
	/// </summary>
	/// <param name="value">The byte value to initialize the <see cref="Bool"/> with.</param>
	public Bool(byte value) : this(Convert.ToBoolean(value)) { }

	/// <summary>
	/// Determines whether the current <see cref="Bool"/> is equal to another <see cref="Bool"/>.
	/// </summary>
	/// <param name="other">The <see cref="Bool"/> struct to compare with the current struct.</param>
	/// <returns><c>true</c> if the current <see cref="Bool"/> is equal to the other <see cref="Bool"/>; otherwise, <c>false</c>.</returns>
	public bool Equals(Bool other)
	{
		return Convert.ToBoolean(_val) == Convert.ToBoolean(other._val);
	}

	/// <summary>
	/// Returns a string representation of the current <see cref="Bool"/>.
	/// </summary>
	/// <returns>A string representation of the current <see cref="Bool"/>.</returns>
	public override string ToString()
	{
		return Convert.ToBoolean(_val).ToString();
	}

	/// <summary>
	/// Implicitly converts a <see cref="Bool"/> to a boolean value.
	/// </summary>
	/// <param name="cb">The <see cref="Bool"/> to convert.</param>
	/// <returns>The boolean value represented by the <see cref="Bool"/>.</returns>
	public static implicit operator bool(Bool cb) => Convert.ToBoolean(cb._val);

	/// <summary>
	/// Implicitly converts a boolean value to <see cref="Bool"/>.
	/// </summary>
	/// <param name="value">The boolean value to convert.</param>
	/// <returns>A <see cref="Bool"/> representing the boolean value.</returns>
	public static implicit operator Bool(bool value) => new(value);
}