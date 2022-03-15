using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using Raylib_cs;
namespace Geostorm.Renderer
{
    class Ui
    {
        public static string[] AimType = { "Mouse Move cursor", "Joystick Move cursor", "Joystick Set Cursor"};
        public static string[] InputStrings = { "Move Up", "Move Left", "Move Down", "Move Right", "Shoot", "Move Cursor Up", "Move Cursor Left", "Move Cursor Down", "Move Cursor Right" };
        public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        public Dictionary<string, Button> buttons = new Dictionary<string, Button>();

        public Ui() { }
        public Ui(GameData.Scene scene, ref GameData.Scene currentScene, GameData data)
        {
            SwitchToScene(scene, ref currentScene, new GameConfig(), data);
        }
        public void Update()
        {
            foreach (var button in buttons)
                button.Value.Update();
        }
        public void Draw(GameData.Scene scene, GameConfig config)
        {
            switch (scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                        DrawText("GeoStorm", GetScreenWidth() / 2 - MeasureText("GeoStorm", 205) / 2, 100, 205, DARKBLUE);
                        DrawText("GeoStorm", GetScreenWidth() / 2 - MeasureText("GeoStorm", 200) / 2, 100, 200, BLUE);
                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                case GameData.Scene.IN_GAME:
                    {

                    }
                    break;
                case GameData.Scene.PAUSE:
                    {
                        DrawRectangleRounded(new Rectangle(GetScreenWidth() / 2 - 400, 300, 800, 200 + buttons.Count() * 150), 0.05f, 1, new Color(150, 150, 150, 255 / 2));
                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        for (int i = 0; i < config.KeyboardInputs.Length; i++)
                        {
                            DrawText(InputStrings[i], 200, 100 + 75 * i, 50, BLUE);
                        }
                        DrawText("Aim Type : ", GetScreenWidth() - 400 - MeasureText("Aim Type : ", 50), GetScreenHeight() - 200, 50, BLUE);
                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                case GameData.Scene.GAME_OVER:
                    {
                        DrawText("GAME OVER", GetScreenWidth() / 2 - MeasureText("GAME OVER", 200)/2, 400, 200, RED);
                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                default:
                    break;
            }
        }
        public void SwitchToScene(GameData.Scene scene, ref GameData.Scene currentScene, GameConfig config, GameData data)
        {
            sprites.Clear();
            buttons.Clear();
            currentScene = scene;
            switch (scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                        data.InitGameData();
                        data.Player = new Core.Entities.Player();
                        buttons["start"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 400), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["start"].SetText("START", new Vector2(15, 0), 100, BLACK);
                        buttons["settings"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 600), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["settings"].SetText("SETTINGS", new Vector2(10, 20), 70, BLACK);
                        buttons["quit"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 800), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["quit"].SetText("QUIT", new Vector2(85, 8), 90, BLACK);
                    }
                    break;
                case GameData.Scene.IN_GAME:
                    {
                        HideCursor();
                    }
                    break;
                case GameData.Scene.PAUSE:
                    {
                        ShowCursor();
                        buttons["resume"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 400), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["resume"].SetText("RESUME", new Vector2(10, 8), 90, BLACK);
                        buttons["quit"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 600), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["quit"].SetText("QUIT", new Vector2(85, 8), 90, BLACK);
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        for (int i = 0; i < config.KeyboardInputs.Length; i++)
                        {
                            buttons["input" + i] = new Button(new Vector2(GetScreenWidth() / 2 + 350, 100 + 75 * i), new Vector2(300, 50), ButtonType.MANUAL, DARKBLUE);
                            buttons["input" + i].SetText("", new Vector2(85, 8), 90, BLACK);
                        }
                        buttons["back"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, GetScreenHeight() - 200), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["back"].SetText("BACK", new Vector2(85, 8), 90, BLACK);
                        buttons["aimtype"] = new Button(new Vector2(GetScreenWidth() - 400, GetScreenHeight() - 200), new Vector2(300, 50), ButtonType.TEXT, DARKBLUE);
                        buttons["aimtype"].SetText(Geostorm.Renderer.Ui.AimType[config.AimType], new Vector2(10, 10), 26, Color.BLACK);
                    }
                    break;
                case GameData.Scene.GAME_OVER:
                    {
                        ShowCursor();
                        buttons["back"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, GetScreenHeight() - 200), new Vector2(400, 100), ButtonType.TEXT, DARKBLUE);
                        buttons["back"].SetText("BACK", new Vector2(85, 8), 90, BLACK);
                        if (data.highscore < data.Score)
                        {
                            data.WriteHighscore(data.Score);
                            data.highscore = data.Score;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
