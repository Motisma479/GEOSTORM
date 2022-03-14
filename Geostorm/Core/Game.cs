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
        public GameData data;
        public GameConfig config;
        public bool ShouldClose = false;
        List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public Game()
        {
            data = new GameData();
            config = new GameConfig();
        }

        public void AddEventListener(IGameEventListener listener)
        {

        }

        public void Update(GameInputs inputs)
        {
            data.ui.Update();
            switch (data.scene)
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
            if (data.ui.buttons["start"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.IN_GAME, ref data.scene, config);
            else if (data.ui.buttons["settings"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.SETTINGS, ref data.scene, config);
            else if (data.ui.buttons["quit"].IsClicked())
                ShouldClose = true;
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                ShouldClose = true;

        }
        static int activebuttons = 0;
        static int timeCount = 0;
        public void UpdateSettings(GameInputs inputs)
        {
            if (data.ui.buttons["back"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref data.scene, config);
            else
            {
                for (int i = 0; i < config.KeyboardInputs.Length; i++)
                {
                    
                    for (int j = 0; j < 5; j++)
                    {
                        //Check if Already key set
                        if (i!= j && config.KeyboardInputs[i].Id == config.KeyboardInputs[j].Id)
                        {
                            config.KeyboardInputs[j].Id = -1;
                            timeCount = 360;
                        }
                    }
                    // Check if other Buttons are clicked
                    if (!data.ui.buttons["input" + activebuttons].IsToggle() || i == activebuttons)
                    {
                        if (data.ui.buttons["input" + i].IsClicked())
                        {
                            data.ui.buttons["input" + i].SetState(true);
                        }
                        if (data.ui.buttons["input" + i].IsToggle() == true)
                        {
                            data.ui.buttons["input" + i].SetText("< Key >", new Vector2(25, 8), 35, Color.BLACK);
                            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE) || config.KeyboardInputs[i].AutoBindKey())
                            {
                                data.ui.buttons["input" + i].SetState(false);
                            }
                        }
                        if (IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) && data.ui.buttons["input" + i].IsClicked() == false)
                            data.ui.buttons["input" + i].SetState(false);
                    }
                    //Convert to key String
                    KeyboardKey tmp = (KeyboardKey)config.KeyboardInputs[i].Id;
                    string key;
                    if (tmp == KeyboardKey.KEY_NULL || tmp == (KeyboardKey)1 || tmp == (KeyboardKey)2)
                    {
                        MouseButton tmp2 = (MouseButton)config.KeyboardInputs[i].Id;
                        key = tmp2.ToString();
                    }
                    else
                    {
                        key = tmp.ToString();
                    }
                    key = key.Replace("KEY", "");
                    key = key.Replace("_", "");
                    // Set Text Button
                    data.ui.buttons["input" + i].SetText(config.KeyboardInputs[i].Id == -1 ? "NONE" : key, new Vector2(5, 8), 26, Color.BLACK);
                }
            }
            if (timeCount > 0)
                timeCount--;
        }

        public void UpdateInGame(GameInputs inputs)
        {
            List<Event> events = new List<Event>();

            // Update Camera
            data.camera.Update(inputs, data.Player.Position, data.MapSize);
            //Update Starts
            for (int i = 0; i < data.stars.Count(); i++)
                data.stars[i].Update(data.camera);
            //Ui Update
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                data.ui.SwitchToScene(GameData.Scene.PAUSE, ref data.scene, config);
            // Update Player
            data.Player.Update(inputs,data,events);
            // Update Bullets
            for (int i = 0; i < data.bullets.Count; i++)
                data.bullets[i].Update(data);

            foreach (IGameEventListener eventListener in eventListeners)
                eventListener.HandleEvents(events, data);
            foreach (var item in data.Grid)
            {
                item.UpdatePoint(data);
            }
            foreach (var item in data.Grid)
            {
                item.UpdatePos();
            }

            data.Synchronize();
        }

        public void UpdatePause(GameInputs inputs)
        {
            if (data.ui.buttons["resume"].IsClicked() || IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                data.ui.SwitchToScene(GameData.Scene.IN_GAME, ref data.scene, config);
            else if (data.ui.buttons["quit"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref data.scene, config);
        }

        public void Render(Graphics graphics, GameInputs inputs)
        {
            switch (data.scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                    }
                    break;
                case GameData.Scene.PAUSE:
                case GameData.Scene.IN_GAME:
                    {
                        // Draw element in the map.
                        foreach (var star in data.stars)
                            if (IsInside(star.Pos + data.camera.Pos * star.Speed))
                                star.Draw(graphics,data.camera);
                        foreach (var point in data.Grid)
                            point.Draw(graphics,data.camera, data.MapSize);
                        DrawRectangleLinesEx(new Rectangle(data.camera.Pos.X, data.camera.Pos.Y, data.MapSize.X, data.MapSize.Y), 5, Color.WHITE);

                        // Draw the Debug.
                        DrawDebug(inputs);

                        // Draw Player
                        data.Player.Draw(graphics, data.camera);

                        for(int i = 0; i < data.bullets.Count; i++)
                        {
                            data.bullets[i].Draw(graphics, data.camera);
                        }

                        //Draw enemies
                        foreach (var enemy in data.enemies)
                            enemy.Draw(graphics, data.camera);

                        DrawFPS(10, 10);
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        if (timeCount > 0)
                            DrawText("You cannot assign a key that is already set", GetScreenWidth() / 2 - MeasureText("You cannot assign a key that is already set", 75) / 2, 100, 75, Color.RED);
                    }
                    break;
                default:
                    break;
            }
            data.ui.Draw(data.scene, config);
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
            DrawCircleV(PosA + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 10, 8, Color.GREEN);
        }
    }
}
