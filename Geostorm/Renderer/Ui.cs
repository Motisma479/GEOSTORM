using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace Geostorm.Renderer
{
    class Ui
    {
        public List<Button> buttons;

        public Ui() { }
        public Ui(GameData.Scene scene)
        {
            buttons = new List<Button>();
            SwitchToScene(scene);
        }
        public void Update()
        {
            for (int i = 0; i < buttons.Count(); i++)
                buttons[i].Update();
        }
        public void Draw(GameData.Scene scene)
        {
            for (int i = 0; i < buttons.Count(); i++)
                buttons[i].Draw();
        }
        public void SwitchToScene(GameData.Scene scene)
        {
            buttons.Clear();
            switch (scene)
            {
                case GameData.Scene.MainMenu:
                    {
                        buttons.Add(new Button(new Vector2(GetScreenWidth()/2 - 200, 200), new Vector2(400,100), Raylib_cs.Color.BLUE, ButtonType.TEXT, "Start Game", 32, new Vector2(10,10), Raylib_cs.Color.WHITE));
                    }
                    break;
                case GameData.Scene.InGame:
                    {
                    }
                    break;
                case GameData.Scene.Pause:
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
