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
        public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        public Dictionary<string, Button> buttons = new Dictionary<string, Button>();

        public Ui() { }
        public Ui(GameData.Scene scene, ref GameData.Scene currentScene)
        {
            SwitchToScene(scene, ref currentScene);
        }
        public void Update()
        {
            foreach (var button in buttons)
                button.Value.Update();
        }
        public void Draw(GameData.Scene scene)
        {
            switch (scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                        DrawText("GeoStorm", GetScreenWidth() / 2 - MeasureText("GeoStorm", 205)/2, 100, 205, DARKBLUE);
                        DrawText("GeoStorm", GetScreenWidth() / 2 - MeasureText("GeoStorm", 200)/2, 100, 200, BLUE);
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
                        DrawRectangleRounded(new Rectangle(GetScreenWidth() / 2 - 400, 300, 800, 200 + buttons.Count() * 150), 0.05f, 1, new Color(150,150,150, 255/2));
                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {

                        foreach (var button in buttons)
                            button.Value.Draw();
                    }
                    break;
                default:
                    break;
            }
        }
        public void SwitchToScene(GameData.Scene scene, ref GameData.Scene currentScene)
        {
            sprites.Clear();
            buttons.Clear();
            currentScene = scene;
            switch (scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                        buttons["start"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 400), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["start"].SetText("START", new Vector2(15,0), 100, DARKBLUE);
                        buttons["settings"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 600), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["settings"].SetText("SETTINGS", new Vector2(10, 20), 70, DARKBLUE);
                        buttons["quit"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 800), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["quit"].SetText("QUIT", new Vector2(85, 8), 90, DARKBLUE);
                    }
                    break;
                case GameData.Scene.IN_GAME:
                    {

                    }
                    break;
                case GameData.Scene.PAUSE:
                    {
                        buttons["resume"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 400), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["resume"].SetText("RESUME", new Vector2(10,8), 90, DARKBLUE);
                        buttons["quit"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, 600), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["quit"].SetText("QUIT", new Vector2(85, 8), 90, DARKBLUE);
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            buttons["input" + i] = new Button(new Vector2(GetScreenWidth() / 2 + 350, 200 + 100 * i), new Vector2(200, 50), ButtonType.MANUAL, BLUE);
                            buttons["input" + i].SetText("", new Vector2(85, 8), 90, DARKBLUE);
                        }
                        buttons["back"] = new Button(new Vector2(GetScreenWidth() / 2 - 200, GetScreenHeight() - 200), new Vector2(400, 100), ButtonType.TEXT, BLUE);
                        buttons["back"].SetText("BACK", new Vector2(85, 8), 90, DARKBLUE);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
