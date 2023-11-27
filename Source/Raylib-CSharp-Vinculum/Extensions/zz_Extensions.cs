
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

using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ZeroElectric.Vinculum.Extensions;

public unsafe static class zz_Extensions
{

	/// <summary>
	/// Pins the specified object in memory and returns a handle to its pinned location.
	/// </summary>
	/// <param name="obj">The object to pin.</param>
	/// <returns>A <see cref="GCHandle"/> representing the pinned object.</returns>	
	public static GCHandle GcPin(this object obj)
	{
		GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
		return handle;
	}

	/// <summary>
	/// Marshals a string to a UTF-8 encoded null-terminated byte array.
	/// </summary>
	/// <param name="text">The string to marshal.</param>
	/// <returns>A <see cref="SpanOwner{sbyte}"/> containing the marshaled byte array.</returns>
	public static SpanOwner<sbyte> MarshalUtf8(this string? text)
	{
		//bugfix: if null is passed, return a zero length span. previously (wrongly) marshaled null as an empty string.
		if (text == null)
		{
			return SpanOwner<sbyte>.Empty;
		}

		int length = Encoding.UTF8.GetByteCount(text) + 1;//need Length+1 so that we always can guarantee a null terminated ending char
		SpanOwner<sbyte> toReturn = SpanOwner<sbyte>.Allocate(length, AllocationMode.Clear);
		int count = Encoding.UTF8.GetBytes(text.AsSpan(), toReturn.Span.AsBytes());
		return toReturn;
	}

	/// <summary>
	/// Converts a <see cref="SpanOwner{TPtr}"/> to a pointer of type <typeparamref name="TPtr"/>.
	/// </summary>
	/// <typeparam name="TPtr">The type of the pointer.</typeparam>
	/// <param name="spanOwner">The <see cref="SpanOwner{TPtr}"/> to convert.</param>
	/// <returns>A pointer of type <typeparamref name="TPtr"/>.</returns>
	public static TPtr* AsPtr<TPtr>(this SpanOwner<TPtr> spanOwner) where TPtr : unmanaged
	{
		if (spanOwner.Length == 0)
		{
			return null;
		}
		return (TPtr*)Unsafe.AsPointer(ref spanOwner.DangerousGetReference());
	}

	/// <summary>
	/// Formats a string using the specified format string and arguments.
	/// </summary>
	/// <param name="format">The format string.</param>
	/// <param name="args">The arguments to format.</param>
	/// <returns>The formatted string.</returns>
	public static string SPrintF(this string format, params object[] args)
	{
		string toReturn = Printf.sprintf(format, args);
		return toReturn;
	}

	/// <summary>
	/// Calculates yaw, pitch, and roll angles.
	/// </summary>
	/// <param name="r">The quaternion to calculate the angles from.</param>
	/// <param name="yaw">The resulting yaw angle.</param>
	/// <param name="pitch">The resulting pitch angle.</param>
	/// <param name="roll">The resulting roll angle.</param>
	/// <remarks>From: LEI-Hongfann: https://github.com/dotnet/runtime/issues/38567#issuecomment-655567603</remarks>
	public static void CreateYawPitchRoll(this Quaternion r, out float yaw, out float pitch, out float roll)
	{
		yaw = MathF.Atan2(2.0f * (r.Y * r.W + r.X * r.Z), 1.0f - 2.0f * (r.X * r.X + r.Y * r.Y));
		pitch = MathF.Asin(2.0f * (r.X * r.W - r.Y * r.Z));
		roll = MathF.Atan2(2.0f * (r.X * r.Y + r.Z * r.W), 1.0f - 2.0f * (r.X * r.X + r.Z * r.Z));
	}

	[Obsolete("Use YawPitchRollAsVector3() instead.")] // TODO REMOVE
	public static Vector3 CreateYawPitchRoll(this Quaternion r)
	{
		return YawPitchRollAsVector3(r);
	}

	/// <summary>
	/// Calculates yaw, pitch, and roll angles.
	/// </summary>
	/// <param name="r">The quaternion to calculate the angles from.</param>
	/// <returns>Vector3 as: yaw, pitch, roll.</returns>
	/// <remarks>From: LEI-Hongfann: https://github.com/dotnet/runtime/issues/38567#issuecomment-655567603</remarks>
	public static Vector3 YawPitchRollAsVector3(this Quaternion r)
	{
		float yaw = MathF.Atan2(2.0f * (r.Y * r.W + r.X * r.Z), 1.0f - 2.0f * (r.X * r.X + r.Y * r.Y));
		float pitch = MathF.Asin(2.0f * (r.X * r.W - r.Y * r.Z));
		float roll = MathF.Atan2(2.0f * (r.X * r.Y + r.Z * r.W), 1.0f - 2.0f * (r.X * r.X + r.Z * r.Z));
		return new(yaw, pitch, roll);
	}
}
