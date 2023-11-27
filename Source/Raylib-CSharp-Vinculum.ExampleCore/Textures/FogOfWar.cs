
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

namespace ZeroElectric.Vinculum.ExampleCore.Textures;

/*******************************************************************************************
*
*   raylib [textures] example - Fog of war
*
*   Example originally created with raylib 4.2, last time updated with raylib 4.2
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2018-2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static class FogOfWar
{
	static int MAP_TILE_SIZE = 32;             // Tiles size 32x32 pixels
	static int PLAYER_SIZE = 16;               // Player size
	static int PLAYER_TILE_VISIBILITY = 2;     // Player can see 2 tiles around its position

	//------------------------------------------------------------------------------------
	// Program main entry point
	//------------------------------------------------------------------------------------
	public static void main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		int screenWidth = 800;
		int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [textures] example - fog of war");

		Map map = new()
		{
			TilesX = 25,
			TilesY = 15
		};

		// NOTE: We can have up to 256 values for tile ids and for tile fog state,
		// probably we don't need that many values for fog state, it can be optimized
		// to use only 2 bits per fog state (reducing size by 4) but logic will be a bit more complex
		map.TileIds = new byte[map.TilesX * map.TilesY];
		map.TileFog = new byte[map.TilesX * map.TilesY];

		// Load map tiles (generating 2 random tile ids for testing)
		// NOTE: Map tile ids should be probably loaded from an external map file
		Random random = new Random();
		for (int i = 0; i < map.TilesY * map.TilesX; i++)
		{
			map.TileIds[i] = (byte)random.Next(0, 2);
		}

		// Player position on the screen (pixel coordinates, not tile coordinates)
		Vector2 playerPosition = new Vector2(180, 130);
		int playerTileX = 0;
		int playerTileY = 0;

		// Render texture to render fog of war
		// NOTE: To get an automatic smooth-fog effect we use a render texture to render fog
		// at a smaller size (one pixel per tile) and scale it on drawing with bilinear filtering
		RenderTexture2D fogOfWar = LoadRenderTexture((int)map.TilesX, (int)map.TilesY);
		SetTextureFilter(fogOfWar.texture, TEXTURE_FILTER_BILINEAR);

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			// Move player around
			if (IsKeyDown(KEY_RIGHT)) playerPosition.X += 5;
			if (IsKeyDown(KEY_LEFT)) playerPosition.X -= 5;
			if (IsKeyDown(KEY_DOWN)) playerPosition.Y += 5;
			if (IsKeyDown(KEY_UP)) playerPosition.Y -= 5;

			// Check player position to avoid moving outside tilemap limits
			if (playerPosition.X < 0)
			{
				playerPosition.X = 0;
			}
			else if ((playerPosition.X + PLAYER_SIZE) > (map.TilesX * MAP_TILE_SIZE))
			{
				playerPosition.X = (float)map.TilesX * MAP_TILE_SIZE - PLAYER_SIZE;
			}

			if (playerPosition.Y < 0)
			{
				playerPosition.Y = 0;
			}
			else if ((playerPosition.Y + PLAYER_SIZE) > (map.TilesY * MAP_TILE_SIZE))
			{
				playerPosition.Y = (float)map.TilesY * MAP_TILE_SIZE - PLAYER_SIZE;
			}

			// Previous visited tiles are set to partial fog
			for (int i = 0; i < map.TilesX * map.TilesY; i++)
			{
				if (map.TileFog[i] == 1)
				{
					map.TileFog[i] = 2;
				}
			}

			// Get current tile position from player pixel position
			playerTileX = (int)((playerPosition.X + (MAP_TILE_SIZE / 2)) / MAP_TILE_SIZE);
			playerTileY = (int)((playerPosition.Y + (MAP_TILE_SIZE / 2)) / MAP_TILE_SIZE);

			// Check visibility and update fog
			// NOTE: We check tilemap limits to avoid processing tiles out-of-array-bounds (it could crash program)
			for (int y = playerTileY - PLAYER_TILE_VISIBILITY; y < (playerTileY + PLAYER_TILE_VISIBILITY); y++)
			{
				for (int x = playerTileX - PLAYER_TILE_VISIBILITY; x < (playerTileX + PLAYER_TILE_VISIBILITY); x++)
				{
					if ((x >= 0) && (x < (int)map.TilesX) && (y >= 0) && (y < (int)map.TilesY))
					{
						map.TileFog[y * map.TilesX + x] = 1;
					}
				}
			}

			// Draw
			//----------------------------------------------------------------------------------

			// Draw fog of war to a small render texture for automatic smoothing on scaling      

			BeginTextureMode(fogOfWar);

			ClearBackground(BLANK);

			for (int y = 0; y < map.TilesY; y++)
			{
				for (int x = 0; x < map.TilesX; x++)
				{
					if (map.TileFog[(y * map.TilesX) + x] == 0)
					{
						DrawRectangle(x, y, 1, 1, BLACK);
					}
					else if (map.TileFog[(y * map.TilesX) + x] == 2)
					{
						DrawRectangle(x, y, 1, 1, Fade(BLACK, 0.8f));
					}
				}
			}

			EndTextureMode();

			BeginDrawing();

			ClearBackground(RAYWHITE);

			for (int y = 0; y < map.TilesY; y++)
			{
				for (int x = 0; x < map.TilesX; x++)
				{
					// Draw tiles from id (and tile borders)
					DrawRectangle(x * MAP_TILE_SIZE, y * MAP_TILE_SIZE, MAP_TILE_SIZE, MAP_TILE_SIZE,
								(map.TileIds[y * map.TilesX + x] == 0) ? BLUE : Fade(BLUE, 0.9f));
					DrawRectangleLines(x * MAP_TILE_SIZE, y * MAP_TILE_SIZE, MAP_TILE_SIZE, MAP_TILE_SIZE, Fade(DARKBLUE, 0.5f));
				}
			}

			// Draw player
			DrawRectangleV(playerPosition, new Vector2(PLAYER_SIZE, PLAYER_SIZE), RED);

			// Draw fog of war (scaled to full map, bilinear filtering)
			DrawTexturePro(fogOfWar.texture,
				new Rectangle(0, 0, fogOfWar.texture.width, -fogOfWar.texture.height),
				new Rectangle(0, 0, map.TilesX * MAP_TILE_SIZE, map.TilesY * MAP_TILE_SIZE),
				new Vector2(0, 0), 0.0f, WHITE);

			// Draw player current tile
			DrawText(TextFormat("Current tile: [%i,%i]", playerTileX, playerTileY), 10, 10, 20, RAYWHITE);
			DrawText("ARROW KEYS to move", 10, screenHeight - 25, 20, RAYWHITE);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadRenderTexture(fogOfWar);      // Unload render texture

		CloseWindow();                      // Close window and OpenGL context

	}

	public class Map
	{
		public int TilesX { get; set; }          // Number of tiles in X axis
		public int TilesY { get; set; }          // Number of tiles in Y axis
		public byte[]? TileIds { get; set; }     // Tile ids (TilesX*TilesY), defines type of tile to draw
		public byte[]? TileFog { get; set; }     // Tile fog state (TilesX*TilesY), defines if a tile has fog or half-fog
	}

}
