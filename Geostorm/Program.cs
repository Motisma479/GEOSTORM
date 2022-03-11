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
            SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(screenWidth, screenHeight, "GeoStorm");
            SetTargetFPS(60);
            SetMousePosition(screenWidth / 2, screenHeight / 2);

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
                ClearBackground(Color.BLACK);
                game.Render(renders, inputs);
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
