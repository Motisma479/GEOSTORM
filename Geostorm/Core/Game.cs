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
        float time;
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
            data.UpdateDeltaTime();
        }

        public void UpdateMainMenu(GameInputs inputs)
        {
            if (data.ui.buttons["start"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.IN_GAME, ref data.scene, config, data);
            else if (data.ui.buttons["settings"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.SETTINGS, ref data.scene, config, data);
            else if (data.ui.buttons["quit"].IsClicked())
                ShouldClose = true;
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                ShouldClose = true;

        }
        static int activebuttons = 0;
        static int timeCount = 0;
        public void UpdateSettings(GameInputs inputs)
        {
            if (data.ui.buttons["aimtype"].IsClicked())
            {
                config.AimType = (config.AimType + 1) % 3;
                data.ui.buttons["aimtype"].SetText(Geostorm.Renderer.Ui.AimType[config.AimType], new Vector2(10, 10), 26, Color.BLACK);
            }

            if (data.ui.buttons["back"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref data.scene, config, data);
            else
            {
                for (int i = 0; i < config.KeyboardInputs.Length; i++)
                {

                    for (int j = 0; j < config.KeyboardInputs.Length; j++)
                    {
                        //Check if Already key set
                        if (i != j && config.KeyboardInputs[i].Id == config.KeyboardInputs[j].Id && config.KeyboardInputs[i].Type == config.KeyboardInputs[j].Type)
                        {
                            config.KeyboardInputs[j].Id = -1;
                            config.KeyboardInputs[j].Type = 0;
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
                    string key = config.KeyboardInputs[i].GetDesc().Replace("_", " ");
                    // Set Text Button
                    data.ui.buttons["input" + i].SetText(config.KeyboardInputs[i].Id == -1 ? "NONE" : key, new Vector2(5, 8), 26, Color.BLACK);
                }
            }
            if (timeCount > 0)
                timeCount--;
        }

        public void UpdateInGame(GameInputs inputs)
        {
            HideCursor();
            List<Event> events = new List<Event>();

            // Update Camera
            data.camera.Update(inputs, data.Player.Position, data.MapSize);
            //Update Starts
            for (int i = 0; i < data.stars.Count(); i++)
                data.stars[i].Update(data.camera);
            //Ui Update
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                data.ui.SwitchToScene(GameData.Scene.PAUSE, ref data.scene, config, data);
            // Update Player
            data.Player.Update(inputs, data, events);
            // Update Entities
            for (int i = 0; i < data.entities.Count; i++)
                data.entities[i].Update(inputs, data, events);

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

            for (int i = 0; i < data.particles.Count(); i++)
            {
                data.particles[i].Update(data);
                if (data.particles[i].time < 0)
                    data.particles.RemoveAt(i);
            }
            foreach (var blackhole in data.blackHoles)
                blackhole.Update(data);
            
            if (data.enemies.Count() <= 0 && data.blackHoles.Count() <= 0 && (time <= 0))
            {
                time = 60/6.0f;
                data.ChangeRound();
            }
            
            data.Synchronize();
            time -= data.DeltaTime;
            Console.WriteLine(time);
        }

        public void UpdatePause(GameInputs inputs)
        {
            ShowCursor();
            if (data.ui.buttons["resume"].IsClicked() || IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                data.ui.SwitchToScene(GameData.Scene.IN_GAME, ref data.scene, config, data);
            else if (data.ui.buttons["quit"].IsClicked())
                data.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref data.scene, config, data);
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
                                star.Draw(graphics, data.camera);
                        foreach (var point in data.Grid)
                            point.Draw(graphics, data.camera, data.MapSize);
                        DrawRectangleLinesEx(new Rectangle(data.camera.Pos.X, data.camera.Pos.Y, data.MapSize.X, data.MapSize.Y), 5, Color.WHITE);

                        foreach (var particle in data.particles)
                        {
                            particle.Draw(graphics, data.camera);
                        }
                        // Draw Player
                        data.Player.Draw(graphics, data.camera);

                        //Draw entities
                        foreach (var entity in data.entities)
                            entity.Draw(graphics, data.camera);

                        foreach (var particle in data.particles)
                        {
                            particle.Draw(graphics, data.camera);
                        }

                        foreach (var blackhole in data.blackHoles)
                            blackhole.Draw(graphics, data.camera);
                        //Draw enemies
                        foreach (var enemy in data.enemies)
                            enemy.Draw(graphics, data.camera);

                        graphics.DrawCursor(inputs.ScreenPos + inputs.ShootTarget);
                        DrawFPS(10, 10);
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        if (timeCount > 0)
                            DrawText("You cannot assign a key that is already set", GetScreenWidth() / 2 - MeasureText("You cannot assign a key that is already set", 75) / 2, 20, 75, Color.RED);
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
    }
}
