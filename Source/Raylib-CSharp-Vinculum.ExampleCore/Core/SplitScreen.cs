
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

/*******************************************************************************************
*
*   raylib [core] example - split screen
*
*   This example has been created using raylib 3.7 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Jeffery Myers (@JeffM2501) and reviewed by Ramon Santamaria (@raysan5)
*
*   Copyright (c) 2021 Jeffery Myers (@JeffM2501)
*
********************************************************************************************/

namespace ZeroElectric.Vinculum.ExampleCore.Core;

public unsafe static class SplitScreen
{
	static Texture2D textureGrid;	// = { 0 };
	static Camera cameraPlayer1;	// = { 0 };
	static Camera cameraPlayer2;	// = { 0 };

	// Scene drawing
	static void DrawScene()
	{
		int count = 5;
		float spacing = 4;

		// Grid of cube trees on a plane to make a "world"
		DrawPlane(new(0, 0, 0), new(50, 50), BEIGE); // Simple world plane

		for (float x = -count * spacing; x <= count * spacing; x += spacing)
		{
			for (float z = -count * spacing; z <= count * spacing; z += spacing)
			{
				DrawCubeTexture(textureGrid, new(x, 1.5f, z), 1, 1, 1, GREEN);
				DrawCubeTexture(textureGrid, new(x, 0.5f, z), 0.25f, 1, 0.25f, BROWN);
			}
		}

		// Draw a cube at each player's position
		DrawCube(cameraPlayer1.position, 1, 1, 1, RED);
		DrawCube(cameraPlayer2.position, 1, 1, 1, BLUE);
	}

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - split screen");

		// Generate a simple texture to use for trees
		Image img = GenImageChecked(256, 256, 32, 32, DARKGRAY, WHITE);
		textureGrid = LoadTextureFromImage(img);
		UnloadImage(img);
		SetTextureFilter(textureGrid, TEXTURE_FILTER_ANISOTROPIC_16X);
		SetTextureWrap(textureGrid, TEXTURE_WRAP_CLAMP);

		// Setup player 1 camera and screen
		cameraPlayer1.fovy = 45.0f;
		cameraPlayer1.up.Y = 1.0f;
		cameraPlayer1.target.Y = 1.0f;
		cameraPlayer1.position.Z = -3.0f;
		cameraPlayer1.position.Y = 1.0f;

		RenderTexture screenPlayer1 = LoadRenderTexture(screenWidth / 2, screenHeight);

		// Setup player two camera and screen
		cameraPlayer2.fovy = 45.0f;
		cameraPlayer2.up.Y = 1.0f;
		cameraPlayer2.target.Y = 3.0f;
		cameraPlayer2.position.X = -3.0f;
		cameraPlayer2.position.Y = 3.0f;

		RenderTexture screenPlayer2 = LoadRenderTexture(screenWidth / 2, screenHeight);

		// Build a flipped rectangle the size of the split view to use for drawing later
		Rectangle splitScreenRect = new(0.0f, 0.0f, (float)screenPlayer1.texture.width, (float)-screenPlayer1.texture.height);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			// If anyone moves this frame, how far will they move based on the time since the last frame
			// this moves thigns at 10 world units per second, regardless of the actual FPS
			float offsetThisFrame = 10.0f * GetFrameTime();

			// Move Player1 forward and backwards (no turning)
			if (IsKeyDown(KEY_W))
			{
				cameraPlayer1.position.Z += offsetThisFrame;
				cameraPlayer1.target.Z += offsetThisFrame;
			}
			else if (IsKeyDown(KEY_S))
			{
				cameraPlayer1.position.Z -= offsetThisFrame;
				cameraPlayer1.target.Z -= offsetThisFrame;
			}

			// Move Player2 forward and backwards (no turning)
			if (IsKeyDown(KEY_UP))
			{
				cameraPlayer2.position.X += offsetThisFrame;
				cameraPlayer2.target.X += offsetThisFrame;
			}
			else if (IsKeyDown(KEY_DOWN))
			{
				cameraPlayer2.position.X -= offsetThisFrame;
				cameraPlayer2.target.X -= offsetThisFrame;
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			// Draw Player1 view to the render texture
			BeginTextureMode(screenPlayer1);
			ClearBackground(SKYBLUE);
			BeginMode3D(cameraPlayer1);
			DrawScene();
			EndMode3D();
			DrawText("PLAYER1 W/S to move", 10, 10, 20, RED);
			EndTextureMode();

			// Draw Player2 view to the render texture
			BeginTextureMode(screenPlayer2);
			ClearBackground(SKYBLUE);
			BeginMode3D(cameraPlayer2);
			DrawScene();
			EndMode3D();
			DrawText("PLAYER2 UP/DOWN to move", 10, 10, 20, BLUE);
			EndTextureMode();

			// Draw both views render textures to the screen side by side
			BeginDrawing();
			ClearBackground(BLACK);
			DrawTextureRec(screenPlayer1.texture, splitScreenRect, new(0, 0), WHITE);
			DrawTextureRec(screenPlayer2.texture, splitScreenRect, new(screenWidth / 2.0f, 0), WHITE);
			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadRenderTexture(screenPlayer1); // Unload render texture
		UnloadRenderTexture(screenPlayer2); // Unload render texture
		UnloadTexture(textureGrid);         // Unload texture

		CloseWindow();                      // Close window and OpenGL context
											//--------------------------------------------------------------------------------------

		return 0;
	}

	//------------------------------------------------------------------------------------
	// Custom Functions Definition
	//------------------------------------------------------------------------------------
	// Draw cube textured
	// NOTE: Cube position is the center position
	static void DrawCubeTexture(Texture2D texture, Vector3 position, float width, float height, float length, Color color)
	{
		float x = position.X;
		float y = position.Y;
		float z = position.Z;

		// Set desired texture to be enabled while drawing following vertex data
		rlSetTexture(texture.id);

		// Vertex data transformation can be defined with the commented lines,
		// but in this example we calculate the transformed vertex data directly when calling rlVertex3f()
		//rlPushMatrix();
		// NOTE: Transformation is applied in inverse order (scale -> rotate -> translate)
		//rlTranslatef(2.0f, 0.0f, 0.0f);
		//rlRotatef(45, 0, 1, 0);
		//rlScalef(2.0f, 2.0f, 2.0f);

		rlBegin(RL_QUADS);
		rlColor4ub(color.r, color.g, color.b, color.a);
		// Front Face
		rlNormal3f(0.0f, 0.0f, 1.0f);       // Normal Pointing Towards Viewer
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width / 2, y - height / 2, z + length / 2);  // Bottom Left Of The Texture and Quad
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width / 2, y - height / 2, z + length / 2);  // Bottom Right Of The Texture and Quad
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width / 2, y + height / 2, z + length / 2);  // Top Right Of The Texture and Quad
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width / 2, y + height / 2, z + length / 2);  // Top Left Of The Texture and Quad
																							  // Back Face
		rlNormal3f(0.0f, 0.0f, -1.0f);     // Normal Pointing Away From Viewer
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width / 2, y - height / 2, z - length / 2);  // Bottom Right Of The Texture and Quad
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width / 2, y + height / 2, z - length / 2);  // Top Right Of The Texture and Quad
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width / 2, y + height / 2, z - length / 2);  // Top Left Of The Texture and Quad
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width / 2, y - height / 2, z - length / 2);  // Bottom Left Of The Texture and Quad
																							  // Top Face
		rlNormal3f(0.0f, 1.0f, 0.0f);       // Normal Pointing Up
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width / 2, y + height / 2, z - length / 2);  // Top Left Of The Texture and Quad
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width / 2, y + height / 2, z + length / 2);  // Bottom Left Of The Texture and Quad
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width / 2, y + height / 2, z + length / 2);  // Bottom Right Of The Texture and Quad
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width / 2, y + height / 2, z - length / 2);  // Top Right Of The Texture and Quad
																							  // Bottom Face
		rlNormal3f(0.0f, -1.0f, 0.0f);     // Normal Pointing Down
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width / 2, y - height / 2, z - length / 2);  // Top Right Of The Texture and Quad
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width / 2, y - height / 2, z - length / 2);  // Top Left Of The Texture and Quad
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width / 2, y - height / 2, z + length / 2);  // Bottom Left Of The Texture and Quad
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width / 2, y - height / 2, z + length / 2);  // Bottom Right Of The Texture and Quad
																							  // Right face
		rlNormal3f(1.0f, 0.0f, 0.0f);       // Normal Pointing Right
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width / 2, y - height / 2, z - length / 2);  // Bottom Right Of The Texture and Quad
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width / 2, y + height / 2, z - length / 2);  // Top Right Of The Texture and Quad
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width / 2, y + height / 2, z + length / 2);  // Top Left Of The Texture and Quad
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width / 2, y - height / 2, z + length / 2);  // Bottom Left Of The Texture and Quad
																							  // Left Face
		rlNormal3f(-1.0f, 0.0f, 0.0f);    // Normal Pointing Left
		rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width / 2, y - height / 2, z - length / 2);  // Bottom Left Of The Texture and Quad
		rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width / 2, y - height / 2, z + length / 2);  // Bottom Right Of The Texture and Quad
		rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width / 2, y + height / 2, z + length / 2);  // Top Right Of The Texture and Quad
		rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width / 2, y + height / 2, z - length / 2);  // Top Left Of The Texture and Quad
		rlEnd();
		//rlPopMatrix();

		rlSetTexture(0);
	}

	// Draw cube with texture piece applied to all faces
	static void DrawCubeTextureRec(Texture2D texture, Rectangle source, Vector3 position, float width, float height, float length, Color color)
	{
		float x = position.X;
		float y = position.Y;
		float z = position.Z;
		float texWidth = (float)texture.width;
		float texHeight = (float)texture.height;

		// Set desired texture to be enabled while drawing following vertex data
		rlSetTexture(texture.id);

		// We calculate the normalized texture coordinates for the desired texture-source-rectangle
		// It means converting from (tex.width, tex.height) coordinates to [0.0f, 1.0f] equivalent 
		rlBegin(RL_QUADS);
		rlColor4ub(color.r, color.g, color.b, color.a);

		// Front face
		rlNormal3f(0.0f, 0.0f, 1.0f);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z + length / 2);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z + length / 2);

		// Back face
		rlNormal3f(0.0f, 0.0f, -1.0f);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z - length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z - length / 2);

		// Top face
		rlNormal3f(0.0f, 1.0f, 0.0f);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z - length / 2);

		// Bottom face
		rlNormal3f(0.0f, -1.0f, 0.0f);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z + length / 2);

		// Right face
		rlNormal3f(1.0f, 0.0f, 0.0f);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z - length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z - length / 2);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x + width / 2, y + height / 2, z + length / 2);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x + width / 2, y - height / 2, z + length / 2);

		// Left face
		rlNormal3f(-1.0f, 0.0f, 0.0f);
		rlTexCoord2f(source.x / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z - length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, (source.y + source.height) / texHeight);
		rlVertex3f(x - width / 2, y - height / 2, z + length / 2);
		rlTexCoord2f((source.x + source.width) / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z + length / 2);
		rlTexCoord2f(source.x / texWidth, source.y / texHeight);
		rlVertex3f(x - width / 2, y + height / 2, z - length / 2);

		rlEnd();

		rlSetTexture(0);
	}
}

