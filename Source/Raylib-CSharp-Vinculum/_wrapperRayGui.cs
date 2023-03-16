
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 4.5.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

using CommunityToolkit.HighPerformance.Buffers;
using global::ZeroElectric.Vinculum.InternalHelpers;
using System.Runtime.InteropServices;

namespace ZeroElectric.Vinculum;

public static unsafe partial class RayGui
{
	public static Boolean GuiWindowBox(Rectangle bounds, string? title)
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

	public static Boolean GuiButton(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiButton(bounds, sotext.AsPtr());
	}

	public static Boolean GuiLabelButton(Rectangle bounds, string? text)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiLabelButton(bounds, sotext.AsPtr());
	}

	public static Boolean GuiToggle(Rectangle bounds, string? text, Boolean active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiToggle(bounds, sotext.AsPtr(), active);
	}

	public static int GuiToggleGroup(Rectangle bounds, string? text, int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiToggleGroup(bounds, sotext.AsPtr(), active);
	}

	public static Boolean GuiCheckBox(Rectangle bounds, string? text, Boolean @checked)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiCheckBox(bounds, sotext.AsPtr(), @checked);
	}

	public static int GuiComboBox(Rectangle bounds, string? text, int active)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiComboBox(bounds, sotext.AsPtr(), active);
	}

	public static Boolean GuiDropdownBox(Rectangle bounds, string? text, int* active, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiDropdownBox(bounds, sotext.AsPtr(), active, editMode);
	}

	public static Boolean GuiSpinner(Rectangle bounds, string? text, int* value, int minValue, int maxValue, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiSpinner(bounds, sotext.AsPtr(), value, minValue, maxValue, editMode);
	}

	public static Boolean GuiValueBox(Rectangle bounds, string? text, int* value, int minValue, int maxValue, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiValueBox(bounds, sotext.AsPtr(), value, minValue, maxValue, editMode);
	}

	public static Boolean GuiTextBox(Rectangle bounds, string? text, int textSize, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiTextBox(bounds, sotext.AsPtr(), textSize, editMode);
	}

	public static Boolean GuiTextBoxMulti(Rectangle bounds, string? text, int textSize, Boolean editMode)
	{
		using SpanOwner<sbyte> sotext = text.MarshalUtf8();
		return GuiTextBoxMulti(bounds, sotext.AsPtr(), textSize, editMode);
	}

	public static float GuiSlider(Rectangle bounds, string? textLeft, string? textRight, float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();
		return GuiSlider(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), value, minValue, maxValue);
	}

	public static float GuiSliderBar(Rectangle rectangle, string? leftText, string? rightText, float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> soTextLeft = leftText.MarshalUtf8();
		using SpanOwner<sbyte> soTextRight = rightText.MarshalUtf8();
		return GuiSliderBar(rectangle, soTextLeft.AsPtr(), soTextRight.AsPtr(), value, minValue, maxValue);
	}

	public static float GuiProgressBar(Rectangle bounds, string? textLeft, string? textRight, float value, float minValue, float maxValue)
	{
		using SpanOwner<sbyte> sotextLeft = textLeft.MarshalUtf8();
		using SpanOwner<sbyte> sotextRight = textRight.MarshalUtf8();
		return GuiProgressBar(bounds, sotextLeft.AsPtr(), sotextRight.AsPtr(), value, minValue, maxValue);
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
		return GuiListView(bounds, sotext.AsPtr(), scrollIndex, active);
	}

	public static int GuiListViewEx(Rectangle bounds, string[] textArray, int count, int* focus, int* scrollIndex, int active)
	{

		sbyte** p_utf8 = stackalloc sbyte*[textArray.Length];
		for (int i = 0; i < textArray.Length; i++)
		{
			p_utf8[i] = (sbyte*)Marshal.StringToCoTaskMemUTF8(textArray[i]);
		}
		int toReturn = GuiListViewEx(bounds, p_utf8, count, focus, scrollIndex, active);

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
