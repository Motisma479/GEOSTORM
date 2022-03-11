using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Geostorm.Core;
using Geostorm.Renderer;
namespace Geostorm
{
    class Program
    {
        static void Main(string[] args)
        {
            const int screenWidth = 1920;
            const int screenHeight = 1080;

            // Initialization
            //--------------------------------------------------------------------------------------
            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(screenWidth, screenHeight, "GeoStorm");
            SetTargetFPS(60);

            InitAudioDevice();

            var game = new Game();
            var inputs = new GameInputs();
            var renders = new Graphics();
            var configs = new GameConfig();
            renders.Load();

            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                float dt = GetFrameTime();
                inputs.Update(configs);
                //TraceLog(TraceLogLevel.LOG_INFO,"Window size: " + inputs.MoveAxis.ToString());
                game.Update(inputs);
                //----------------------------------------------------------------------------------


                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();
                ClearBackground(Color.WHITE);
                renders.DrawPlayer(System.Numerics.Vector2.One, 0);
                DrawCircle(100, 100, 10, Color.GRAY);
                DrawCircle((int)(100 + inputs.MoveAxis.X * 10), (int)(100 + inputs.MoveAxis.Y * 10), 8, Color.GREEN);
                DrawEllipse(150,100,25,15,Color.GRAY);
                if (inputs.Shoot) DrawEllipse(150,100,23,13,Color.GREEN);
                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
