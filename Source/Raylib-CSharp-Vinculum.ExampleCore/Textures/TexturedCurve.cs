
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

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - Draw a texture along a segmented curve
*
*   Example originally created with raylib 4.5, last time updated with raylib 4.5
*
*   Example contributed by Jeffery Myers and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2022-2023 Jeffery Myers and Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public unsafe static class TexturedCurve
{
	static Texture2D texRoad = new Texture2D();

	static bool showCurve = false;

	static float curveWidth = 50;
	static int curveSegments = 24;

	static Vector2 curveStartPosition = new Vector2();
	static Vector2 curveStartPositionTangent = new Vector2();

	static Vector2 curveEndPosition = new Vector2();
	static Vector2 curveEndPositionTangent = new Vector2();

	static Vector2* curveSelectedPoint = null;

	public unsafe static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		SetConfigFlags(FLAG_VSYNC_HINT | FLAG_MSAA_4X_HINT);
		InitWindow(screenWidth, screenHeight, "raylib [textures] examples - textured curve");

		// Load the road texture
		texRoad = LoadTexture("resources/road.png");
		SetTextureFilter(texRoad, TEXTURE_FILTER_BILINEAR);

		// Setup the curve
		curveStartPosition = new Vector2(80, 100);
		curveStartPositionTangent = new Vector2(100, 300);

		curveEndPosition = new Vector2(700, 350);
		curveEndPositionTangent = new Vector2(600, 100);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// Curve config options
			if (IsKeyPressed(KEY_SPACE)) showCurve = !showCurve;
			if (IsKeyPressed(KEY_EQUAL)) curveWidth += 2;
			if (IsKeyPressed(KEY_MINUS)) curveWidth -= 2;
			if (curveWidth < 2) curveWidth = 2;

			// Update segments
			if (IsKeyPressed(KEY_LEFT)) curveSegments -= 2;
			if (IsKeyPressed(KEY_RIGHT)) curveSegments += 2;

			if (curveSegments < 2) curveSegments = 2;

			// Update curve logic
			// If the mouse is not down, we are not editing the curve so clear the selection
			if (!IsMouseButtonDown(MOUSE_LEFT_BUTTON))
			{
				curveSelectedPoint = null;
			}

			// If a point was selected, move it
			if (curveSelectedPoint is not null)
			{
				*curveSelectedPoint = *curveSelectedPoint + GetMouseDelta();
			}

			// The mouse is down, and nothing was selected, so see if anything was picked
			Vector2 mouse = GetMousePosition();

			fixed (Vector2* p = &curveStartPosition, p2 = &curveStartPositionTangent, p3 = &curveEndPosition, p4 = &curveEndPositionTangent)
			{
				if (CheckCollisionPointCircle(mouse, curveStartPosition, 6))
				{
					curveSelectedPoint = p;
				}
				else if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6))
				{
					curveSelectedPoint = p2;
				}
				else if (CheckCollisionPointCircle(mouse, curveEndPosition, 6))
				{
					curveSelectedPoint = p3;
				}
				else if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6))
				{
					curveSelectedPoint = p4;
				}
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawTexturedCurve();    // Draw a textured Spline Cubic Bezier

			// Draw spline for reference
			if (showCurve)
			{
				DrawSplineSegmentBezierCubic(curveStartPosition, curveEndPosition, curveStartPositionTangent, curveEndPositionTangent, 2, BLUE);
			}

			// Draw the various control points and highlight where the mouse is
			DrawLineV(curveStartPosition, curveStartPositionTangent, SKYBLUE);
			DrawLineV(curveStartPositionTangent, curveEndPositionTangent, Fade(LIGHTGRAY, 0.4f));
			DrawLineV(curveEndPosition, curveEndPositionTangent, PURPLE);

			if (CheckCollisionPointCircle(mouse, curveStartPosition, 6)) DrawCircleV(curveStartPosition, 7, YELLOW);
			DrawCircleV(curveStartPosition, 5, RED);

			if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6)) DrawCircleV(curveStartPositionTangent, 7, YELLOW);
			DrawCircleV(curveStartPositionTangent, 5, MAROON);

			if (CheckCollisionPointCircle(mouse, curveEndPosition, 6)) DrawCircleV(curveEndPosition, 7, YELLOW);
			DrawCircleV(curveEndPosition, 5, GREEN);

			if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6)) DrawCircleV(curveEndPositionTangent, 7, YELLOW);
			DrawCircleV(curveEndPositionTangent, 5, DARKGREEN);


			// Draw usage info
			DrawText("Drag points to move curve, press SPACE to show/hide base curve", 10, 10, 10, DARKGRAY);
			DrawText(TextFormat("Curve width: %2.0f (Use + and - to adjust)", curveWidth), 10, 30, 10, DARKGRAY);
			DrawText(TextFormat("Curve segments: %d (Use LEFT and RIGHT to adjust)", curveSegments), 10, 50, 10, DARKGRAY);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadTexture(texRoad);

		CloseWindow();        // Close window and OpenGL context

	}

	// Draw textured curve using Spline Cubic Bezier
	static void DrawTexturedCurve()
	{
		float step = 1.0f / curveSegments;

		Vector2 previous = curveStartPosition;
		Vector2 previousTangent = new Vector2();
		float previousV = 0;

		// We can't compute a tangent for the first point, so we need to reuse the tangent from the first segment
		bool tangentSet = false;

		Vector2 current = new Vector2();
		float t = 0.0f;

		for (int i = 1; i <= curveSegments; i++)
		{
			t = step * i;

			float a = (float)Math.Pow(1.0f - t, 3);
			float b = 3.0f * (float)Math.Pow(1.0f - t, 2) * t;
			float c = 3.0f * (1.0f - t) * (float)Math.Pow(t, 2);
			float d = (float)Math.Pow(t, 3);

			// Compute the endpoint for this segment
			current.Y = a * curveStartPosition.Y + b * curveStartPositionTangent.Y + c * curveEndPositionTangent.Y + d * curveEndPosition.Y;
			current.X = a * curveStartPosition.X + b * curveStartPositionTangent.X + c * curveEndPositionTangent.X + d * curveEndPosition.X;

			// Vector from previous to current
			Vector2 delta = new Vector2(current.X - previous.X, current.Y - previous.Y);

			// The right hand normal to the delta vector
			Vector2 normal = Vector2.Normalize(new Vector2(-delta.Y, delta.X));

			// The v texture coordinate of the segment (add up the length of all the segments so far)
			float v = previousV + delta.Length();

			// Make sure the start point has a normal
			if (!tangentSet)
			{
				previousTangent = normal;
				tangentSet = true;
			}

			// Extend out the normals from the previous and current points to get the quad for this segment
			Vector2 prevPosNormal = previous + Vector2.Multiply(previousTangent, curveWidth);
			Vector2 prevNegNormal = previous + Vector2.Multiply(previousTangent, -curveWidth);

			Vector2 currentPosNormal = current + Vector2.Multiply(normal, curveWidth);
			Vector2 currentNegNormal = current + Vector2.Multiply(normal, -curveWidth);

			// Draw the segment as a quad
			rlSetTexture(texRoad.id);
			rlBegin(RL_QUADS);
			rlColor4ub(255, 255, 255, 255);
			rlNormal3f(0.0f, 0.0f, 1.0f);

			rlTexCoord2f(0, previousV);
			rlVertex2f(prevNegNormal.X, prevNegNormal.Y);

			rlTexCoord2f(1, previousV);
			rlVertex2f(prevPosNormal.X, prevPosNormal.Y);

			rlTexCoord2f(1, v);
			rlVertex2f(currentPosNormal.X, currentPosNormal.Y);

			rlTexCoord2f(0, v);
			rlVertex2f(currentNegNormal.X, currentNegNormal.Y);
			rlEnd();

			// The current step is the start of the next step
			previous = current;
			previousTangent = normal;
			previousV = v;
		}
	}
}
