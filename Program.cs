using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Geostorm.Core;
using Geostorm.Renderer;
namespace Geostorm
{
    class Program
    {

        private static void InitializeWindow()
        {
            SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(1600, 900, "GeoStorm");
            int monitor = GetCurrentMonitor();
            SetWindowSize((int)(GetMonitorWidth(monitor) * 0.75f), (int)(GetMonitorHeight(monitor) * 0.75f));
            SetWindowPosition((int)(GetMonitorWidth(monitor) * 0.1f), (int)(GetMonitorHeight(monitor) * 0.1f));
            Image icon = LoadImage("Assets/GEOSTORM.png");
            SetWindowIcon(icon);
            SetTargetFPS(60);
            SetMousePosition(GetScreenWidth() / 2, GetScreenHeight() / 2);
            SetExitKey(0);
            InitAudioDevice();
        }

        static void Main(string[] args)
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            InitializeWindow();

            var game = new Game();
            game.LoadConfigFile();

            var inputs = new GameInputs();
            inputs.LocalSize = game.gameData.MapSize;

            Shader bloomShader = LoadShader("", "Assets/Shaders/bloom.fs");
            //--------------------------------------------------------------------------------------
            RenderTexture2D renderTexture = LoadRenderTexture(GetScreenWidth(),GetScreenHeight());

            // Main game loop
            while (!WindowShouldClose() && !game.ShouldClose)
            {
                // Update
                //----------------------------------------------------------------------------------
                Vector2 size = new Vector2(GetScreenWidth(), GetScreenHeight());

                if (size != inputs.ScreenSize)
                {
                    UnloadRenderTexture(renderTexture);
                    renderTexture = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
                }

                if (game.gameData.scene != GameData.Scene.PAUSE)
                    inputs.Update(game.config, game.gameData.player.Position);
                game.Update(inputs);
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------

                // Draw Scene
                BeginTextureMode(renderTexture);
                ClearBackground(Color.BLACK);
                game.Render(inputs);
                EndTextureMode();

                // Draw FrameBuffer Rect
                BeginDrawing();
                ClearBackground(Color.BLACK);
                BeginShaderMode(bloomShader);
                DrawTextureRec(renderTexture.texture, new Rectangle( 0, 0, (float)renderTexture.texture.width, (float)-renderTexture.texture.height), new Vector2(0, 0), Color.WHITE);
                EndShaderMode();
                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            game.WriteConfigFile();

            UnloadShader(bloomShader);
            UnloadRenderTexture(renderTexture);
            CloseAudioDevice();
            CloseWindow();
            //--------------------------------------------------------------------------------------
        }
    }
}
