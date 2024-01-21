
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

namespace ZeroElectric.Vinculum.ExampleCore.Models;

//********************************************************************************************/
//*
//* raylib[models] example - Mesh picking in 3d mode, ground plane, triangle, mesh
//*
//* This example has been created using raylib 1.7 (www.raylib.com)
//* raylib is licensed under an unmodified zlib/libpng license(View raylib.h for details)
//*
//* Example contributed by Joel Davis(@joeld42) and reviewed by Ramon Santamaria(@raysan5)
//*
//* Copyright(c) 2017 Joel Davis(@joeld42) and Ramon Santamaria(@raysan5)
//*
//********************************************************************************************/

public unsafe static class MeshPicking
{

	const float FLT_MAX = float.MaxValue;

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------

		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [models] example - mesh picking");

		// Define the camera to look into our 3d world
		Camera camera = new();
		camera.position = new(20.0f, 20.0f, 20.0f);          // Camera position
		camera.target = new(0.0f, 8.0f, 0.0f);               // Camera looking at point
		camera.up = new(0.0f, 1.6f, 0.0f);                   // Camera up vector (rotation towards target)
		camera.fovy = 45.0f;                                 // Camera field-of-view Y
		camera.Projection = CAMERA_PERSPECTIVE;             // Camera mode type

		Ray ray = new();        // Picking ray

		Model tower = LoadModel("resources/models/wavefront/turret.obj");                   // Load OBJ model
		Texture2D texture = LoadTexture("resources/models/wavefront/turret_diffuse.png");   // Load model texture
		tower.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;               // Set model diffuse texture

		Vector3 towerPos = new(0.0f, 0.0f, 0.0f);                           // Set model position
		BoundingBox towerBBox = GetMeshBoundingBox(tower.meshes[0]);        // Get mesh bounding box

		// Ground quad
		Vector3 g0 = new(-50.0f, 0.0f, -50.0f);
		Vector3 g1 = new(-50.0f, 0.0f, 50.0f);
		Vector3 g2 = new(50.0f, 0.0f, 50.0f);
		Vector3 g3 = new(50.0f, 0.0f, -50.0f);

		// Test triangle
		Vector3 ta = new(-25.0f, 0.5f, 0.0f);
		Vector3 tb = new(-4.0f, 2.5f, 1.0f);
		Vector3 tc = new(-8.0f, 6.5f, 0.0f);

		Vector3 bary = new(0.0f, 0.0f, 0.0f);

		// Test sphere
		Vector3 sp = new(-30.0f, 5.0f, 5.0f);
		float sr = 4.0f;

		// Limit cursor to relative movement inside the window
		DisableCursor();

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

		// Main game loop, 'WindowShouldClose' Detects window close button or ESC key
		//----------------------------------------------------------------------------------
		while (!WindowShouldClose())
		{
			// Update
			//----------------------------------------------------------------------------------

			UpdateCamera(ref camera, CAMERA_FREE);          // Update camera

			// Display information about closest hit
			RayCollision collision = new();
			string hitObjectName = "None";
			collision.distance = FLT_MAX;
			collision.hit = false;
			Color cursorColor = WHITE;

			// Get ray and test against objects
			ray = GetMouseRay(GetMousePosition(), camera);

			// Check ray collision against ground quad
			RayCollision groundHitInfo = GetRayCollisionQuad(ray, g0, g1, g2, g3);

			if ((groundHitInfo.hit) && (groundHitInfo.distance < collision.distance))
			{
				collision = groundHitInfo;
				cursorColor = GREEN;
				hitObjectName = "Ground";
			}

			// Check ray collision against test triangle
			RayCollision triHitInfo = GetRayCollisionTriangle(ray, ta, tb, tc);

			if ((triHitInfo.hit) && (triHitInfo.distance < collision.distance))
			{
				collision = triHitInfo;
				cursorColor = PURPLE;
				hitObjectName = "Triangle";

				bary = Vector3Barycenter(collision.point, ta, tb, tc);
			}

			// Check ray collision against test sphere
			RayCollision sphereHitInfo = GetRayCollisionSphere(ray, sp, sr);

			if ((sphereHitInfo.hit) && (sphereHitInfo.distance < collision.distance))
			{
				collision = sphereHitInfo;
				cursorColor = ORANGE;
				hitObjectName = "Sphere";
			}

			// Check ray collision against bounding box first, before trying the full ray-mesh test
			RayCollision boxHitInfo = GetRayCollisionBox(ray, towerBBox);

			if ((boxHitInfo.hit) && (boxHitInfo.distance < collision.distance))
			{
				collision = boxHitInfo;
				cursorColor = ORANGE;
				hitObjectName = "Box";

				// Check ray collision against model meshes
				RayCollision meshHitInfo = default;
				for (int m = 0; m < tower.meshCount; m++)
				{
					// NOTE: We consider the model.transform for the collision check but 
					// it can be checked against any transform Matrix, used when checking against same
					// model drawn multiple times with multiple transforms
					meshHitInfo = GetRayCollisionMesh(ray, tower.meshes[m], tower.transform);
					if (meshHitInfo.hit)
					{
						// Save the closest hit mesh
						if ((!collision.hit) || (collision.distance > meshHitInfo.distance)) collision = meshHitInfo;

						break;  // Stop once one mesh collision is detected, the colliding mesh is m
					}
				}

				if (meshHitInfo.hit)
				{
					collision = meshHitInfo;
					cursorColor = ORANGE;
					hitObjectName = "Mesh";
				}
			}

			// Draw
			//----------------------------------------------------------------------------------

			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode3D(camera);

			// Draw the tower
			// WARNING: If scale is different than 1.0f,
			// not considered by GetRayCollisionModel()
			DrawModel(tower, towerPos, 1.0f, WHITE);

			// Draw the test triangle
			DrawLine3D(ta, tb, PURPLE);
			DrawLine3D(tb, tc, PURPLE);
			DrawLine3D(tc, ta, PURPLE);

			// Draw the test sphere
			DrawSphereWires(sp, sr, 8, 8, PURPLE);

			// Draw the mesh bbox if we hit it
			if (boxHitInfo.hit) DrawBoundingBox(towerBBox, LIME);

			// If we hit something, draw the cursor at the hit point
			if (collision.hit)
			{
				DrawCube(collision.point, 0.3f, 0.3f, 0.3f, cursorColor);
				DrawCubeWires(collision.point, 0.3f, 0.3f, 0.3f, RED);

				Vector3 normalEnd;
				normalEnd.X = collision.point.X + collision.normal.X;
				normalEnd.Y = collision.point.Y + collision.normal.Y;
				normalEnd.Z = collision.point.Z + collision.normal.Z;

				DrawLine3D(collision.point, normalEnd, RED);
			}

			DrawRay(ray, MAROON);

			DrawGrid(10, 10.0f);

			EndMode3D();

			// Draw some debug GUI text
			DrawText(TextFormat("Hit Object: %s", hitObjectName), 10, 50, 10, BLACK);

			if (collision.hit)
			{
				int ypos = 70;

				DrawText(TextFormat("Distance: %3.2f", collision.distance), 10, ypos, 10, BLACK);

				DrawText(TextFormat("Hit Pos: %3.2f %3.2f %3.2f",
									collision.point.X,
									collision.point.Y,
									collision.point.Z), 10, ypos + 15, 10, BLACK);

				DrawText(TextFormat("Hit Norm: %3.2f %3.2f %3.2f",
									collision.normal.X,
									collision.normal.Y,
									collision.normal.Z), 10, ypos + 30, 10, BLACK);

				if (triHitInfo.hit && TextIsEqual(hitObjectName, "Triangle"))
					DrawText(TextFormat("Barycenter: %3.2f %3.2f %3.2f", bary.X, bary.Y, bary.Z), 10, ypos + 45, 10, BLACK);
			}

			DrawText("Use Mouse to Move Camera", 10, 430, 10, GRAY);

			DrawText("(c) Turret 3D model by Alberto Cano", screenWidth - 200, screenHeight - 20, 10, GRAY);

			DrawFPS(10, 10);

			EndDrawing();
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------

		UnloadModel(tower);         // Unload model
		UnloadTexture(texture);     // Unload texture

		CloseWindow();              // Close window and OpenGL context

		return 0;
	}
}
