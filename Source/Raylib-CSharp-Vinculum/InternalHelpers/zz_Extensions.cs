
//------------------------------------------------------------------------------
//
// Copyright 2022 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 4.2.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace ZeroElectric.Vinculum.InternalHelpers;

public unsafe static class zz_Extensions
{
	public static GCHandle GcPin(this object obj)
	{
		GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
		return handle;
	}

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
	/// if an empty span is supplied, marshalls a NULL pointer.
	/// </summary>
	/// <typeparam name="TPtr"></typeparam>
	/// <param name="spanOwner"></param>
	/// <returns></returns>
	public static TPtr* AsPtr<TPtr>(this SpanOwner<TPtr> spanOwner) where TPtr : unmanaged
	{
		if (spanOwner.Length == 0)
		{
			return null;
		}
		return (TPtr*)Unsafe.AsPointer(ref spanOwner.DangerousGetReference());
	}

	public static string SPrintF(this string format, params object[] args)
	{
		string toReturn = Printf.sprintf(format, args);
		return toReturn;
	}

	public static void CreateYawPitchRoll(this Quaternion r, out float yaw, out float pitch, out float roll)
	{
		//implementation from: LEI-Hongfann: https://github.com/dotnet/runtime/issues/38567#issuecomment-655567603
		yaw = MathF.Atan2(2.0f * (r.Y * r.W + r.X * r.Z), 1.0f - 2.0f * (r.X * r.X + r.Y * r.Y));
		pitch = MathF.Asin(2.0f * (r.X * r.W - r.Y * r.Z));
		roll = MathF.Atan2(2.0f * (r.X * r.Y + r.Z * r.W), 1.0f - 2.0f * (r.X * r.X + r.Z * r.Z));
	}

	public static Vector3 CreateYawPitchRoll(this Quaternion r)
	{
		//implementation from: LEI-Hongfann: https://github.com/dotnet/runtime/issues/38567#issuecomment-655567603
		float yaw = MathF.Atan2(2.0f * (r.Y * r.W + r.X * r.Z), 1.0f - 2.0f * (r.X * r.X + r.Y * r.Y));
		float pitch = MathF.Asin(2.0f * (r.X * r.W - r.Y * r.Z));
		float roll = MathF.Atan2(2.0f * (r.X * r.Y + r.Z * r.W), 1.0f - 2.0f * (r.X * r.X + r.Z * r.Z));
		return new(yaw, pitch, roll);
	}
}
