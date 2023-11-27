
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
using System.Numerics;
using System.Runtime.InteropServices;
using ZeroElectric.Vinculum.Extensions;

namespace ZeroElectric.Vinculum;

public static unsafe partial class RayGui
{
	public static int GuiWindowBox(Rectangle bounds, string? title)
	{
		using SpanOwner<sbyte> sotitle = title.MarshalUtf8();
		return GuiWindowBox(bounds, sotitle.AsPtr());
	}

	public static int GuiGroupBox(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiGroupBox(bounds, sotext.AsPtr());
	}

	public static int GuiLine(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiLine(bounds, sotext.AsPtr());
	}

	public static int GuiLabel(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiLabel(bounds, sotext.AsPtr());
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

	public static int GuiToggle(Rectangle bounds, string? text, ref bool active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (bool* activePtr = &active)
			return GuiToggle(bounds, sotext.AsPtr(), (Bool*)activePtr);
	}

	public static int GuiToggleGroup(Rectangle bounds, string? text, ref int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (int* activePtr = &active)
			return GuiToggleGroup(bounds, sotext.AsPtr(), activePtr);
	}

	public static int GuiCheckBox(Rectangle bounds, string? text, ref bool @checked)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();

		fixed (bool* activePtr = &@checked)
			return GuiCheckBox(bounds, sotext.AsPtr(), (Bool*)activePtr);
	}

	public static int GuiComboBox(Rectangle bounds, string? text, ref int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (int* activePtr = &active)
			return GuiComboBox(bounds, sotext.AsPtr(), activePtr);
	}

	public static int GuiDropdownBox(Rectangle bounds, string? text, ref int active, bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (int* activePtr = &active)
			return GuiDropdownBox(bounds, sotext.AsPtr(), activePtr, editMode);
	}

	public static int GuiSpinner(Rectangle bounds, string? text, ref int value, int minValue, int maxValue, bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (int* valuePtr = &value)
			return GuiSpinner(bounds, sotext.AsPtr(), valuePtr, minValue, maxValue, editMode);
	}

	public static int GuiValueBox(Rectangle bounds, string? text, ref int value, int minValue, int maxValue, bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		fixed (int* valuePtr = &value)
			return GuiValueBox(bounds, sotext.AsPtr(), valuePtr, minValue, maxValue, editMode);
	}

	public static int GuiTextBox(Rectangle bounds, string? text, int textSize, bool editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiTextBox(bounds, sotext.AsPtr(), textSize, editMode);
	}

	public static int GuiSlider(Rectangle bounds, string? textLeft, string? textRight, ref float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();
		fixed (float* valuePtr = &value)
			return GuiSlider(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), valuePtr, minValue, maxValue);
	}

	public static int GuiSliderBar(Rectangle rectangle, string? leftText, string? rightText, ref float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> soTextLeft = leftText.MarshalUtf8();
		using SpanOwner<sbyte> soTextRight = rightText.MarshalUtf8();

		fixed (float* val = &value)
			return GuiSliderBar(rectangle, soTextLeft.AsPtr(), soTextRight.AsPtr(), val, minValue, maxValue);
	}

	public static int GuiProgressBar(Rectangle bounds, string? textLeft, string? textRight, ref float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();

		fixed (float* valuePtr = &value)
			return GuiProgressBar(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), valuePtr, minValue, maxValue);
	}

	public static int GuiStatusBar(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiStatusBar(bounds, sotext.AsPtr());
	}

	public static int GuiDummyRec(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiDummyRec(bounds, sotext.AsPtr());
	}

	public static int GuiListView(Rectangle bounds, string? text, ref int scrollIndex, ref int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();

		fixed (int* scrollIndexPtr = &scrollIndex)
		fixed (int* activePtr = &active)
			return GuiListView(bounds, sotext.AsPtr(), scrollIndexPtr, activePtr);
	}

	public static int GuiListViewEx(Rectangle bounds, string[] text, int count, ref int scrollIndex, ref int active, ref int focus)
	{
		int toReturn;

		sbyte** p_utf8 = stackalloc sbyte*[text.Length];
		for (int i = 0; i < text.Length; i++)
		{
			p_utf8[i] = (sbyte*)Marshal.StringToCoTaskMemUTF8(text[i]);
		}

		fixed (int* scrollIndexPtr = &scrollIndex)
		fixed (int* activePtr = &active)
		fixed (int* focusPtr = &focus)
		{
			toReturn = GuiListViewEx(bounds, p_utf8, count, scrollIndexPtr, activePtr, focusPtr);
		}

		for (int i = 0; i < text.Length; i++)
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

	public static int GuiToggleSlider(Rectangle bounds, string text, ref int active)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (int* activePtr = &active)
			return GuiToggleSlider(bounds, soText.AsPtr(), activePtr);
	}

	public static int GuiColorPickerHSV(Rectangle bounds, string text, ref Vector3 hsvColor)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Vector3* hsvPtr = &hsvColor)
			return GuiColorPanelHSV(bounds, soText.AsPtr(), hsvPtr);
	}

	public static int GuiColorPanelHSV(Rectangle bounds, string text, ref Vector3 hsvColor)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Vector3* hsvPtr = &hsvColor)
			return GuiColorPanelHSV(bounds, soText.AsPtr(), hsvPtr);
	}

	public static int GuiScrollPanel(Rectangle bounds, string text, Rectangle content, ref Vector2 scroll, ref Rectangle view)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Vector2* scrollPtr = &scroll)
		fixed (Rectangle* viewPtr = &view)
			return GuiScrollPanel(bounds, soText.AsPtr(), content, scrollPtr, viewPtr);
	}

	public static int GuiColorPicker(Rectangle bounds, string text, ref Color color)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Color* colorPtr = &color)
			return GuiColorPicker(bounds, soText.AsPtr(), colorPtr);
	}

	public static int GuiColorPanel(Rectangle bounds, string text, ref Color color)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Color* colorPtr = &color)
			return GuiColorPanel(bounds, soText.AsPtr(), colorPtr);
	}

	public static int GuiColorBarAlpha(Rectangle bounds, string text, ref float alpha)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (float* alphaPtr = &alpha)
			return GuiColorBarAlpha(bounds, soText.AsPtr(), alphaPtr);
	}

	public static int GuiColorBarHue(Rectangle bounds, string text, ref float hue)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (float* huePtr = &hue)
			return GuiColorBarHue(bounds, soText.AsPtr(), huePtr);
	}

	public static int GuiGrid(Rectangle bounds, string text, float spacing, int subdivs, ref Vector2 mouseCell)
	{
		using SpanOwner<sbyte> soText = text.MarshalUtf8();
		fixed (Vector2* mouseCellPtr = &mouseCell)
			return GuiGrid(bounds, soText.AsPtr(), spacing, subdivs, mouseCellPtr);
	}
}
