
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

using CommunityToolkit.HighPerformance.Buffers;
using global::ZeroElectric.Vinculum.Extensions;
using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum;

public static unsafe partial class RayGui
{
	public static int GuiWindowBox(Rectangle bounds, string? title)
	{
		using SpanOwner<sbyte> sotitle = title.MarshalUtf8();
		return GuiWindowBox(bounds, sotitle.AsPtr());

	}

	public static void GuiGroupBox(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		GuiGroupBox(bounds, sotext.AsPtr());
	}

	public static void GuiLine(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		GuiLine(bounds, sotext.AsPtr());
	}

	public static void GuiLabel(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		GuiLabel(bounds, sotext.AsPtr());
	}

	public static int GuiButton(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiButton(bounds, sotext.AsPtr());
	}

	public static int GuiLabelButton(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiLabelButton(bounds, sotext.AsPtr());
	}

	public static int GuiToggle(Rectangle bounds, string? text, Bool active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiToggle(bounds, sotext.AsPtr(), &active);
	}

	public static int GuiToggleGroup(Rectangle bounds, string? text, int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiToggleGroup(bounds, sotext.AsPtr(), &active);
	}

	
	public static bool GuiCheckBox(Rectangle bounds, string? text, Bool @checked) //TODO (KEN) Fix bool* -> Bool* for correct implementation
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();

		GuiCheckBox(bounds, sotext.AsPtr(), &@checked);

		return @checked;
	}

	public static int GuiComboBox(Rectangle bounds, string? text, int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiComboBox(bounds, sotext.AsPtr(), &active);
	}

	public static int GuiDropdownBox(Rectangle bounds, string? text, int* active, bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiDropdownBox(bounds, sotext.AsPtr(), active, editMode);
	}

	public static int GuiSpinner(Rectangle bounds, string? text, int* value, int minValue, int maxValue, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiSpinner(bounds, sotext.AsPtr(), value, minValue, maxValue, editMode);
	}

	public static int GuiValueBox(Rectangle bounds, string? text, int* value, int minValue, int maxValue, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiValueBox(bounds, sotext.AsPtr(), value, minValue, maxValue, editMode);
	}

	public static int GuiTextBox(Rectangle bounds, string? text, int textSize, Bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiTextBox(bounds, sotext.AsPtr(), textSize, editMode);
	}

	public static float GuiSlider(Rectangle bounds, string? textLeft, string? textRight, float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();
		return GuiSlider(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), &value, minValue, maxValue);
	}

	public static float GuiSliderBar(Rectangle rectangle, string? leftText, string? rightText, ref float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> soTextLeft = leftText.MarshalUtf8();
		using SpanOwner<sbyte> soTextRight = rightText.MarshalUtf8();

		fixed(float* val = &value)
		return GuiSliderBar(rectangle, soTextLeft.AsPtr(), soTextRight.AsPtr(), val, minValue, maxValue);
	}

	public static int GuiProgressBar(Rectangle bounds, string? textLeft, string? textRight, float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();

		return GuiProgressBar(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), &value, minValue, maxValue);
	}

	public static void GuiStatusBar(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		GuiStatusBar(bounds, sotext.AsPtr());
	}

	public static void GuiDummyRec(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		GuiDummyRec(bounds, sotext.AsPtr());
	}

	public static int GuiListView(Rectangle bounds, string? text, int* scrollIndex, int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiListView(bounds, sotext.AsPtr(), scrollIndex, &active);
	}

	public static int GuiListViewEx(Rectangle bounds, string[] textArray, int count, int* scrollIndex, int* active, int* focus)
	{

		sbyte** p_utf8 = stackalloc sbyte*[textArray.Length];
		for (int i = 0; i < textArray.Length; i++)
		{
			p_utf8[i] = (sbyte*)Marshal.StringToCoTaskMemUTF8(textArray[i]);
		}

		int toReturn = GuiListViewEx(bounds, p_utf8, count, scrollIndex, active, focus);

		for (int i = 0; i < textArray.Length; i++)
		{
			Marshal.ZeroFreeCoTaskMemUTF8((IntPtr)p_utf8[i]);
		}

		return toReturn;
	}

	public static int GuiMessageBox(Rectangle bounds, string? title, string? message, string? buttons)
	{
		using SpanOwner<sbyte> soTitle = title.MarshalUtf8();
		using SpanOwner<sbyte> soMessage = message.MarshalUtf8();
		using SpanOwner<sbyte> sobuttons = buttons.MarshalUtf8();
		return GuiMessageBox(bounds, soTitle.AsPtr(), soMessage.AsPtr(), sobuttons.AsPtr());
	}

	public static int GuiTextInputBox(Rectangle bounds, string? title, string? message, string? buttons, string? text, int textMaxSize = 255)
	{
		using SpanOwner<sbyte> soTitle = title.MarshalUtf8();
		using SpanOwner<sbyte> soMessage = message.MarshalUtf8();
		using SpanOwner<sbyte> sobuttons = buttons.MarshalUtf8();
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiTextInputBox(bounds, soTitle.AsPtr(), soMessage.AsPtr(), sobuttons.AsPtr(), sotext.AsPtr(), textMaxSize, null);
	}

	public static void GuiLoadStyle(string? fileName)
	{
		using SpanOwner<sbyte> sofileName = fileName.MarshalUtf8();
		GuiLoadStyle(sofileName.AsPtr());
	}

	public static string GuiIconText(int iconId, string? text)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		return Helpers.Utf8ToString(GuiIconText(iconId, soText.AsPtr()));
	}
}
