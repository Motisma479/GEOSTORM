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

            var datas = new GameData();
            var inputs = new GameInputs();
            var renders = new Graphics();

            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())
            {
                // Update
                //----------------------------------------------------------------------------------
                float dt = GetFrameTime();
                datas.game.Update(inputs);
                //----------------------------------------------------------------------------------


                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();
                ClearBackground(Color.WHITE);
                datas.game.Render(renders);
                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            renders.Unload();
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
