
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

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum.Extensions;

/// <summary>
/// helper marshalling struct convert byte sized CBOOL's to/from dotnet
/// <para>You should be able to ignore this type.   treat it just as you would a normal bool.</para>
/// </summary>
/// <remarks> see here for some reasoning behind this type: https://docs.microsoft.com/en-us/dotnet/standard/native-interop/customize-struct-marshaling#customizing-boolean-field-marshaling
/// <para>the native structs that use bool expect it to be the size of byte, so this type lets it be that, and yet east for managed code to treat it as a boolean</para></remarks>
[DebuggerDisplay("{ToString(),raw}")]
public readonly partial struct Bool : IEquatable<Bool>
{
	/// <summary>
	/// private to prevent abuse.   Managed code should treat this as a bool.
	/// </summary>
	[MarshalAs(UnmanagedType.U1)] //probably not needed
	private readonly byte _val;

	public Bool(bool value)
	{
		_val = Convert.ToByte(value);
	}


	public Bool(byte value) : this(Convert.ToBoolean(value)) //any non-zero should be treated the same, so lets coherse it to == 1
	{
		//_val = value;
	}

	public bool Equals(Bool other)
	{
		//because emulate bool, compare only the related bits (non-zero == true)
		return Convert.ToBoolean(_val) == Convert.ToBoolean(other._val);
	}

	public override string ToString()
	{
		return Convert.ToBoolean(_val).ToString();
	}

	public static implicit operator bool(Bool cb) => Convert.ToBoolean(cb._val);
	public static implicit operator Bool(bool value) => new(value);

	//// bad idea to allow conversion to/from byte, as that is likely a bug/misuse.   so the following conversions are disabled:
	//public static implicit operator byte(Bool cb) => Convert.ToByte(cb._val);
	//public static implicit operator Bool(byte value) => new(value);

}
