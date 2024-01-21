////------------------------------------------------------------------------------
////
//// Copyright 2022-2024 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
//// This file is licensed to you under the MPL-2.0.
//// See the LICENSE file in the project's root for more info.
////
//// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
//// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
//// Find raylib here: https://github.com/raysan5/raylib
////
////------------------------------------------------------------------------------

namespace ZeroElectric.Vinculum.ExampleCore.Shapes;

/*******************************************************************************************
*
*   raylib [shapes] example - splines drawing
*
*   Example originally created with raylib 5.0, last time updated with raylib 5.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static class SplinesDrawing
{
	public unsafe static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int MAX_SPLINE_POINTS = 32;

		const int screenWidth = 800;
		const int screenHeight = 450;

		SetConfigFlags(FLAG_MSAA_4X_HINT);
		InitWindow(screenWidth, screenHeight, "raylib [shapes] example - splines drawing");

		Vector2[] points = new Vector2[MAX_SPLINE_POINTS];
		points[0] = new Vector2(50.0f, 400.0f);
		points[1] = new Vector2(160.0f, 220.0f);
		points[2] = new Vector2(340.0f, 380.0f);
		points[3] = new Vector2(520.0f, 60.0f);
		points[4] = new Vector2(710.0f, 260.0f);

		int pointCount = 5;
		int selectedPoint = -1;
		int focusedPoint = -1;

		Vector2* selectedControlPoint = null;
		Vector2* focusedControlPoint = null;

		ControlPoint[] control = new ControlPoint[MAX_SPLINE_POINTS];

		for (int i = 0; i < pointCount; i++)
		{
			control[i].Start = new Vector2(points[i].X + 50, points[i].Y);
			control[i].End = new Vector2(points[i + 1].X - 50, points[i + 1].Y);
		}

		// Spline config variables
		float splineThickness = 8.0f;
		int splineTypeActive = (int)SplineType.SPLINE_LINEAR; // 0-Linear, 1-BSpline, 2-CatmullRom, 3-Bezier
		bool splineTypeEditMode = false;
		bool splineHelpersActive = true;

		// Set our game to run at 60 frames-per-second
		SetTargetFPS(60);

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// Spline points creation logic (at the end of spline)
			if (IsMouseButtonPressed(MOUSE_RIGHT_BUTTON) && (pointCount < MAX_SPLINE_POINTS))
			{
				points[pointCount] = GetMousePosition();
				pointCount++;
			}

			// Spline point focus and selection logic
			for (int i = 0; i < pointCount; i++)
			{
				if (CheckCollisionPointCircle(GetMousePosition(), points[i], 8.0f))
				{
					focusedPoint = i;
					if (IsMouseButtonDown(MOUSE_LEFT_BUTTON)) selectedPoint = i;
					break;
				}
				else focusedPoint = -1;
			}

			// Spline point movement logic
			if (selectedPoint >= 0)
			{
				points[selectedPoint] = GetMousePosition();
				if (IsMouseButtonReleased(MOUSE_LEFT_BUTTON)) selectedPoint = -1;
			}

			// Cubic Bezier spline control points logic
			if ((splineTypeActive == (int)SplineType.SPLINE_BEZIER) && (focusedPoint == -1))
			{
				// Spline control point focus and selection logic
				for (int i = 0; i < pointCount; i++)
				{
					if (CheckCollisionPointCircle(GetMousePosition(), control[i].Start, 6.0f))
					{
						fixed (Vector2* vecPtr = &control[i].Start)
						{
							focusedControlPoint = vecPtr;
							if (IsMouseButtonDown(MOUSE_LEFT_BUTTON)) selectedControlPoint = vecPtr;

						}
						break;
					}
					else if (CheckCollisionPointCircle(GetMousePosition(), control[i].End, 6.0f))
					{
						fixed (Vector2* vecPtr = &control[i].End)
						{
							focusedControlPoint = vecPtr;
							if (IsMouseButtonDown(MOUSE_LEFT_BUTTON)) selectedControlPoint = vecPtr;
						}
						break;
					}
					else focusedControlPoint = null;
				}

				// Spline control point movement logic
				if (selectedControlPoint != null)
				{
					*selectedControlPoint = GetMousePosition();
					if (IsMouseButtonReleased(MOUSE_LEFT_BUTTON)) selectedControlPoint = null;
				}

				// Spline selection logic
				if (IsKeyPressed(KEY_ONE)) splineTypeActive = 0;
				else if (IsKeyPressed(KEY_TWO)) splineTypeActive = 1;
				else if (IsKeyPressed(KEY_THREE)) splineTypeActive = 2;
				else if (IsKeyPressed(KEY_FOUR)) splineTypeActive = 3;
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			if (splineTypeActive == (int)SplineType.SPLINE_LINEAR)
			{
				fixed (Vector2* vetPtr = points)
				{
					// Draw spline: linear
					DrawSplineLinear(vetPtr, pointCount, splineThickness, RED);
				}
			}
			else if (splineTypeActive == (int)SplineType.SPLINE_BASIS)
			{
				// Draw spline: basis
				fixed (Vector2* vetPtr = points)
				{
					DrawSplineBasis(vetPtr, pointCount, splineThickness, RED);  // Provide connected points array
				}
			}
			else if (splineTypeActive == (int)SplineType.SPLINE_CATMULLROM)
			{
				// Draw spline: catmull-rom
				fixed (Vector2* vetPtr = points)
				{
					DrawSplineCatmullRom(vetPtr, pointCount, splineThickness, RED); // Provide connected points array
				}
			}
			else if (splineTypeActive == (int)SplineType.SPLINE_BEZIER)
			{
				// Draw spline: cubic-bezier (with control points)
				for (int i = 0; i < pointCount - 1; i++)
				{
					// Drawing individual segments, not considering thickness connection compensation
					DrawSplineSegmentBezierCubic(points[i], control[i].Start, control[i].End, points[i + 1], splineThickness, RED);

					// Every cubic bezier point should have two control points
					DrawCircleV(control[i].Start, 6, GOLD);
					DrawCircleV(control[i].End, 6, GOLD);

					fixed (ControlPoint* pointPtr = &control[i])
					{
						if (focusedControlPoint == &pointPtr->Start) DrawCircleV(control[i].Start, 8, GREEN);
						else if (focusedControlPoint == &pointPtr->End) DrawCircleV(control[i].End, 8, GREEN);
					}

					DrawLineEx(points[i], control[i].Start, 1.0f, LIGHTGRAY);
					DrawLineEx(points[i + 1], control[i].End, 1.0f, LIGHTGRAY);

					// Draw spline control lines
					DrawLineV(points[i], control[i].Start, GRAY);
					//DrawLineV(control[i].start, control[i].end, LIGHTGRAY);
					DrawLineV(control[i].End, points[i + 1], GRAY);
				}
			}

			if (splineHelpersActive)
			{
				// Draw spline point helpers
				for (int i = 0; i < pointCount; i++)
				{
					DrawCircleLinesV(points[i], (focusedPoint == i) ? 12.0f : 8.0f, (focusedPoint == i) ? BLUE : DARKBLUE);
					if ((splineTypeActive != (int)SplineType.SPLINE_LINEAR) &&
						(splineTypeActive != (int)SplineType.SPLINE_BEZIER) &&
						(i < pointCount - 1)) DrawLineV(points[i], points[i + 1], GRAY);

					DrawText(TextFormat("[%.0f, %.0f]", points[i].X, points[i].Y), points[i].X, points[i].Y + 10, 10, BLACK);
				}
			}

			// Check all possible UI states that require controls lock
			if (splineTypeEditMode) GuiLock();

			// Draw spline config
			GuiLabel(new Rectangle(12, 62, 140, 24), string.Format("Spline thickness: {0}", (int)splineThickness));
			GuiSliderBar(new Rectangle(12, 60 + 24, 140, 16), null, null, ref splineThickness, 1.0f, 40.0f);

			GuiCheckBox(new Rectangle(12, 110, 20, 20), "Show point helpers", ref splineHelpersActive);
			GuiUnlock();

			GuiLabel(new Rectangle(12, 10, 140, 24), "Spline type:");
			if (GuiDropdownBox(new Rectangle(12, 8 + 24, 140, 28), "LINEAR;BSPLINE;CATMULLROM;BEZIER", ref splineTypeActive, splineTypeEditMode) == 1)
			{
				splineTypeEditMode = !splineTypeEditMode;
			}

			EndDrawing();

		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		CloseWindow();        // Close window and OpenGL context

	}

	internal struct ControlPoint
	{
		public Vector2 Start;
		public Vector2 End;
	}

	internal enum SplineType
	{
		SPLINE_LINEAR = 0,      // Linear
		SPLINE_BASIS,           // B-Spline
		SPLINE_CATMULLROM,      // Catmull-Rom
		SPLINE_BEZIER           // Cubic Bezier
	}
}
