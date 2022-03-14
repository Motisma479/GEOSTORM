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
        public GameData datas;
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
            datas.ui.Update();
            switch (datas.scene)
            {
                case GameData.Scene.MAIN_MENU:
                    UpdateMainMenu(inputs);
                    break;
                case GameData.Scene.IN_GAME:
                    UpdateInGame(inputs);
                    break;
                case GameData.Scene.PAUSE:
                    UpdatePause(inputs);
                    break;
                case GameData.Scene.SETTINGS:
                    UpdateSettings(inputs);
                    break;
                default:
                    break;
            }
        }

        public void UpdateMainMenu(GameInputs inputs)
        {
            if (datas.ui.buttons["start"].IsClicked())
                datas.ui.SwitchToScene(GameData.Scene.IN_GAME, ref datas.scene);
            else if (datas.ui.buttons["settings"].IsClicked())
                datas.ui.SwitchToScene(GameData.Scene.SETTINGS, ref datas.scene);
            else if (datas.ui.buttons["quit"].IsClicked())
                System.Environment.Exit(1);
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                System.Environment.Exit(1);

        }
        static int activebuttons = 0;
        public void UpdateSettings(GameInputs inputs)
        {
            if (datas.ui.buttons["back"].IsClicked())
                datas.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref datas.scene);
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (!datas.ui.buttons["input" + activebuttons].IsToggle())
                    {
                        if (datas.ui.buttons["input" + i].IsClicked())
                        {
                            datas.ui.buttons["input" + i].SetState(true);
                        }
                        if (datas.ui.buttons["input" + i].IsToggle() == true)
                        {
                            datas.ui.buttons["input" + i].SetText("< Key >", new Vector2(25, 8), 35, Color.BLACK);
                        }
                    }
                }
            }
        }

        public void UpdateInGame(GameInputs inputs)
        {
            List<Event> events = new List<Event>();

            datas.camera.Update(inputs, datas.Player.Position, datas.MapSize);
            for (int i = 0; i < datas.stars.Count(); i++)
                datas.stars[i].Update(datas.camera);
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                datas.ui.SwitchToScene(GameData.Scene.PAUSE, ref datas.scene);
            datas.Player.Update(inputs,datas,events);
            for (int i = 0; i < datas.bullets.Count; i++)
                datas.bullets[i].Update(datas);

            foreach (IGameEventListener eventListener in eventListeners)
                eventListener.HandleEvents(events, datas);
            foreach (var item in datas.Grid)
            {
                item.UpdatePoint(datas.Player.Position);
            }
            foreach (var item in datas.Grid)
            {
                item.UpdatePos();
            }
        }

        public void UpdatePause(GameInputs inputs)
        {
            if (datas.ui.buttons["resume"].IsClicked() || IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                datas.ui.SwitchToScene(GameData.Scene.IN_GAME, ref datas.scene);
            else if (datas.ui.buttons["quit"].IsClicked())
                datas.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref datas.scene);
        }

        public void Render(Graphics graphics, GameInputs inputs)
        {
            switch (datas.scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                    }
                    break;
                case GameData.Scene.PAUSE:
                case GameData.Scene.IN_GAME:
                    {
                        // Draw element in the map.
                        foreach (var star in datas.stars)
                            if (IsInside(star.Pos + datas.camera.Pos * star.Speed))
                                star.Draw(graphics,datas.camera);
                        foreach (var point in datas.Grid)
                            point.Draw(graphics,datas.camera);

                        // Draw the Debug.
                        DrawDebug(inputs);

                        // Draw Player
                        datas.Player.Draw(graphics, datas.camera);

                        for(int i = 0; i < datas.bullets.Count; i++)
                        {
                            datas.bullets[i].Draw(graphics, datas.camera);
                        }

                        //Draw enemies
                        foreach (var enemy in datas.enemies)
                            enemy.Draw(graphics, datas.camera);

                        DrawFPS(10, 10);
                    }
                    break;
                default:
                    break;
            }
            datas.ui.Draw(datas.scene);
        }

        public bool IsInside(Vector2 pos)
        {
            if ((pos.X < GetScreenWidth() && pos.X > 0) && (pos.Y < GetScreenHeight() && pos.Y > 0))
                return true;
            else
                return false;
        }

        public void DrawDebug(GameInputs inputs)
        {
            Vector2 PosA = new Vector2(100, 100);
            DrawCircleV(PosA, 10, Color.GRAY);
            DrawCircleV(PosA + inputs.MoveAxis * 10, 8, Color.GREEN);
            DrawCircleV(inputs.ScreenPos + inputs.ShootTarget, 8, Color.GREEN);
            DrawEllipse(150, 100, 25, 15, Color.GRAY);
            if (inputs.Shoot) DrawEllipse(150, 100, 23, 13, Color.GREEN);
            PosA = new Vector2(200, 100);
            DrawCircleV(PosA, 10, Color.GRAY);
            DrawCircleV(PosA + MathHelper.GetVectorRot(datas.Player.WeaponRotation) * 10, 8, Color.GREEN);
        }
    }
}
