using System;
using Raylib_cs;
using Geostorm.Core;
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
            Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.InitWindow(screenWidth, screenHeight, "GeoStorm");
            Raylib.SetTargetFPS(60);

            Raylib.InitAudioDevice();

            var game = new GameData();

            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!Raylib.WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                float dt = Raylib.GetFrameTime();
                game.Update();
                //----------------------------------------------------------------------------------


                // Draw
                //----------------------------------------------------------------------------------
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
