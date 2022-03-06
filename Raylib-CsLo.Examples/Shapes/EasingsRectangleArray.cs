// Copyright ©️ Raylib-CsLo and Contributors.
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project root for more info.
// The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo

namespace Raylib_CsLo.Examples.Shapes;

/*******************************************************************************************
*
*   raylib [shapes] example - easings rectangle array
*
*   NOTE: This example requires 'easings.h' library, provided on raylib/src. Just copy
*   the library to same directory as example or make sure it's available on include path.
*
*   This example has been created using raylib 2.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2014-2019 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static unsafe class EasingsRectangleArray
{

    // # include "extras/easings.h"            // Required for easing functions

    const int RECS_WIDTH = 50;
    const int RECS_HEIGHT = 50;

    const int MAX_RECS_X = 800 / RECS_WIDTH;
    const int MAX_RECS_Y = 450 / RECS_HEIGHT;

    const int PLAY_TIME_IN_FRAMES = 240;                 // At 60 fps = 4 seconds

    public static int Example()
    {
        // Initialization

        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [shapes] example - easings rectangle array");

        Rectangle[] recs = new Rectangle[MAX_RECS_X * MAX_RECS_Y];

        for (int y = 0; y < MAX_RECS_Y; y++)
        {
            for (int x = 0; x < MAX_RECS_X; x++)
            {
                recs[(y * MAX_RECS_X) + x].X = (RECS_WIDTH / 2.0f) + (RECS_WIDTH * x);
                recs[(y * MAX_RECS_X) + x].Y = (RECS_HEIGHT / 2.0f) + (RECS_HEIGHT * y);
                recs[(y * MAX_RECS_X) + x].width = RECS_WIDTH;
                recs[(y * MAX_RECS_X) + x].height = RECS_HEIGHT;
            }
        }

        float rotation = 0.0f;
        int framesCounter = 0;
        int state = 0;                  // Rectangles animation state: 0-Playing, 1-Finished

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second


        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            if (state == 0)
            {
                framesCounter++;

                for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                {
                    recs[i].height = EaseCircOut(framesCounter, RECS_HEIGHT, -RECS_HEIGHT, PLAY_TIME_IN_FRAMES);
                    recs[i].width = EaseCircOut(framesCounter, RECS_WIDTH, -RECS_WIDTH, PLAY_TIME_IN_FRAMES);

                    if (recs[i].height < 0)
                    {
                        recs[i].height = 0;
                    }

                    if (recs[i].width < 0)
                    {
                        recs[i].width = 0;
                    }

                    if ((recs[i].height == 0) && (recs[i].width == 0))
                    {
                        state = 1;   // Finish playing
                    }

                    rotation = EaseLinearIn(framesCounter, 0.0f, 360.0f, PLAY_TIME_IN_FRAMES);
                }
            }
            else if ((state == 1) && IsKeyPressed(KEY_SPACE))
            {
                // When animation has finished, press space to restart
                framesCounter = 0;

                for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                {
                    recs[i].height = RECS_HEIGHT;
                    recs[i].width = RECS_WIDTH;
                }

                state = 0;
            }


            // Draw

            BeginDrawing();

            ClearBackground(RAYWHITE);

            if (state == 0)
            {
                for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                {
                    DrawRectanglePro(recs[i], new Vector2(recs[i].width / 2, recs[i].height / 2), rotation, RED);
                }
            }
            else if (state == 1)
            {
                DrawText("PRESS [SPACE] TO PLAY AGAIN!", 240, 200, 20, GRAY);
            }

            EndDrawing();

        }

        // De-Initialization

        CloseWindow();        // Close window and OpenGL context


        return 0;
    }
}
