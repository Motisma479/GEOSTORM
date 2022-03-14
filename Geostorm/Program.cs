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
            SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(screenWidth, screenHeight, "GeoStorm");
            SetTargetFPS(60);
            SetMousePosition(screenWidth / 2, screenHeight / 2);
            SetExitKey(0);
            InitAudioDevice();

            var game = new Game();
            var inputs = new GameInputs();
            inputs.LocalSize = game.data.MapSize;
            var renders = new Graphics();
            renders.Load();
            game.config.LoadConfigFile();
            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose() && !game.ShouldClose)
            {
                // Update
                //----------------------------------------------------------------------------------
                float dt = GetFrameTime();
                inputs.Update(game.config, game.data.Player.Position);
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
            game.config.WriteConfigFile();
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
