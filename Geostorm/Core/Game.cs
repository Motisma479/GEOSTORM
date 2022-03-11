using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;
using Geostorm.Renderer;
using System.Numerics;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Geostorm.Core
{
    class Game
    {
        GameData datas;
        List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public Game()
        {
            datas = new GameData();
        }

        public void AddEventListener(IGameEventListener listener)
        {

        }
        public void Update(GameInputs inputs)
        {
            switch (datas.scene)
            {
                case GameData.Scene.MainMenu:
                    UpdateMainMenu(inputs);
                    break;
                case GameData.Scene.InGame:
                    UpdateInGame(inputs);
                    break;
                case GameData.Scene.Pause:
                    UpdatePause(inputs);
                    break;
                default:
                    break;
            }
        }
        public void UpdateMainMenu(GameInputs inputs)
        {
            if (datas.ui.buttons.First().IsClicked())
            {
                datas.scene = GameData.Scene.InGame;
                datas.ui.SwitchToScene(GameData.Scene.InGame);
            }
        }

        public void UpdateInGame(GameInputs inputs)
        {
            datas.camera.Update(inputs, datas.Player.Position, datas.MapSize);
            for (int i = 0; i < datas.stars.Count(); i++)
                datas.stars[i].Update(datas.camera);
            datas.Player.Update(inputs,datas.MapSize);
        }

        public void UpdatePause(GameInputs inputs)
        {

        }

        public void Render(Graphics graphics, GameInputs inputs)
        {
            datas.ui.Draw(datas.scene);
            switch (datas.scene)
            {
                case GameData.Scene.MainMenu:
                    {

                    }
                    break;
                case GameData.Scene.InGame:
                    {
                        for (int i = 0; i < datas.stars.Count(); i++)
                            //if (IsInside(datas.camera.Pos + datas.stars[i].Pos))
                                datas.stars[i].Draw(graphics,datas.camera);
                        graphics.DrawMap(datas.MapSize, datas.camera);
                        Vector2 PosA = new Vector2(100, 100);
                        DrawCircleV(PosA, 10, Color.GRAY);
                        DrawCircleV(PosA + inputs.MoveAxis * 10, 8, Color.GREEN);
                        DrawCircleV(inputs.ScreenPos + inputs.ShootTarget, 8, Color.GREEN);
                        DrawEllipse(150, 100, 25, 15, Color.GRAY);
                        if (inputs.Shoot) DrawEllipse(150, 100, 23, 13, Color.GREEN);
                        PosA = new Vector2(200, 100);
                        DrawCircleV(PosA, 10, Color.GRAY);
                        DrawCircleV(PosA + MathHelper.getVectorRot(datas.Player.WeaponRotation) * 10, 8, Color.GREEN);
                        datas.Player.Draw(graphics,datas.camera);
                        DrawFPS(10, 10);
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

        public bool IsInside(Vector2 pos)
        {
            if ((pos.X < GetScreenWidth() && pos.X > 0) && (pos.Y < GetScreenHeight() && pos.Y > 0))
                return true;
            else
                return false;
        }
    }
}
