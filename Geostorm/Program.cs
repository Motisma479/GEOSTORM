using System.Numerics;
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
            Shader bloomShader = LoadShader("", "Assets/Shaders/bloom.fs");
            //--------------------------------------------------------------------------------------
            RenderTexture2D target = LoadRenderTexture(GetScreenWidth(),GetScreenHeight());

            // Main game loop
            while (!WindowShouldClose() && !game.ShouldClose)
            {
                // Update
                //----------------------------------------------------------------------------------
                Vector2 size = new Vector2(GetScreenWidth(), GetScreenHeight());
                if (size != inputs.ScreenSize)
                {
                    UnloadRenderTexture(target);
                    target = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
                }
                float dt = GetFrameTime();
                inputs.Update(game.config, game.data.Player.Position);
                game.Update(inputs);
                
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------
                BeginTextureMode(target);
                ClearBackground(Color.BLACK);
                game.Render(renders, inputs);
                EndTextureMode();
                BeginDrawing();
                ClearBackground(Color.BLACK);
                BeginShaderMode(bloomShader);
                DrawTextureRec(target.texture, new Rectangle( 0, 0, (float)target.texture.width, (float)-target.texture.height), new Vector2(0, 0), Color.WHITE);
                EndShaderMode();
                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            game.config.WriteConfigFile();
            UnloadShader(bloomShader);
            UnloadRenderTexture(target);
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
