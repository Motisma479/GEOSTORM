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

            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                game.Update(inputs);
                //----------------------------------------------------------------------------------


                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();
                ClearBackground(Color.BLACK);
                game.Render(renders);
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
