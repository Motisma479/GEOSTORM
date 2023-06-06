using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Renderer;
using System.Numerics;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Geostorm.Core
{
    class Game
    {
        public GameData gameData;
        public GameConfig config;
        public bool ShouldClose = false;
        float time;

        public Game()
        {
            gameData = new GameData();
            config = new GameConfig();
        }

        public void Update(GameInputs inputs)
        {
            gameData.ui.Update();
            switch (gameData.scene)
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
                case GameData.Scene.GAME_OVER:
                    UpdateGameOver(inputs);
                    break;
                default:
                    break;
            }
            gameData.UpdateDeltaTime();
        }

        public void UpdateMainMenu(GameInputs inputs)
        {
            if (gameData.ui.buttons["start"].IsClicked())
                gameData.ui.SwitchToScene(GameData.Scene.IN_GAME, ref gameData.scene, config, gameData);
            else if (gameData.ui.buttons["settings"].IsClicked())
                gameData.ui.SwitchToScene(GameData.Scene.SETTINGS, ref gameData.scene, config, gameData);
            else if (gameData.ui.buttons["quit"].IsClicked())
                ShouldClose = true;
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                ShouldClose = true;
        }

        static int activebuttons = 0;
        static int timeCount = 0;

        public void UpdateSettings(GameInputs inputs)
        {
            if (gameData.ui.buttons["aimtype"].IsClicked())
            {
                config.AimType = (config.AimType + 1) % 3;
                gameData.ui.buttons["aimtype"].SetText(Geostorm.Renderer.Ui.AimType[config.AimType], new Vector2(10, 10), 26, Color.BLACK);
            }

            if (gameData.ui.buttons["back"].IsClicked())
                gameData.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref gameData.scene, config, gameData);
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
                    if (!gameData.ui.buttons["input" + activebuttons].IsToggle() || i == activebuttons)
                    {
                        if (gameData.ui.buttons["input" + i].IsClicked())
                        {
                            gameData.ui.buttons["input" + i].SetState(true);
                        }
                        if (gameData.ui.buttons["input" + i].IsToggle() == true)
                        {
                            gameData.ui.buttons["input" + i].SetText("< Key >", new Vector2(25, 8), 35, Color.BLACK);
                            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE) || config.KeyboardInputs[i].AutoBindKey())
                            {
                                gameData.ui.buttons["input" + i].SetState(false);
                            }
                        }
                        if (IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) && gameData.ui.buttons["input" + i].IsClicked() == false)
                            gameData.ui.buttons["input" + i].SetState(false);
                    }
                    //Convert to key String
                    KeyboardKey tmp = (KeyboardKey)config.KeyboardInputs[i].Id;
                    string key = config.KeyboardInputs[i].GetDesc().Replace("_", " ");
                    // Set Text Button
                    gameData.ui.buttons["input" + i].SetText(config.KeyboardInputs[i].Id == -1 ? "NONE" : key, new Vector2(5, 8), 26, Color.BLACK);
                }
            }
            if (timeCount > 0)
                timeCount--;
        }

        public void UpdateInGame(GameInputs inputs)
        {
            // Update Camera
            gameData.camera.Update(inputs, gameData.player.Position, gameData.MapSize);

            //Update Stars
            for (int i = 0; i < gameData.stars.Count(); i++)
                gameData.stars[i].Update(gameData.camera);

            //Ui Update
            if (IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                gameData.ui.SwitchToScene(GameData.Scene.PAUSE, ref gameData.scene, config, gameData);

            // Update Player
            gameData.player.Update(inputs, gameData);

            // Update Entities
            foreach (var entity in gameData.entities)
                entity.Update(inputs, gameData);

            gameData.grid.Update(gameData);

            for (int i = 0; i < gameData.particles.Count(); i++)
            {
                gameData.particles[i].Update(gameData);
                if (gameData.particles[i].time < 0)
                    gameData.particles.RemoveAt(i);
            }

            foreach (var blackhole in gameData.blackHoles)
                blackhole.Update(gameData);

            if (gameData.enemies.Count() <= 0 && gameData.blackHoles.Count() <= 0 && (time <= 0))
            {
                time = 60 / 6.0f;
                gameData.ChangeRound();
            }

            gameData.Synchronize();
            time -= gameData.DeltaTime;

            if (gameData.player.Life <= 0)
            {
                gameData.ui.SwitchToScene(GameData.Scene.GAME_OVER, ref gameData.scene, config, gameData);
            }
        }

        public void UpdatePause(GameInputs inputs)
        {
            if (gameData.ui.buttons["resume"].IsClicked() || IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                gameData.ui.SwitchToScene(GameData.Scene.IN_GAME, ref gameData.scene, config, gameData);
            else if (gameData.ui.buttons["quit"].IsClicked())
                gameData.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref gameData.scene, config, gameData);
        }

        public void UpdateGameOver(GameInputs inputs)
        {
            if (gameData.ui.buttons["back"].IsClicked())
                gameData.ui.SwitchToScene(GameData.Scene.MAIN_MENU, ref gameData.scene, config, gameData);
        }

        public void Render(GameInputs inputs)
        {
            switch (gameData.scene)
            {
                case GameData.Scene.MAIN_MENU:
                    {
                    }
                    break;
                case GameData.Scene.PAUSE:
                case GameData.Scene.IN_GAME:
                    {
                        // Draw element in the map.
                        foreach (var star in gameData.stars)
                            if (IsInside(star.position + gameData.camera.Pos * star.speed))
                                star.Draw(gameData.camera);

                        gameData.grid.Draw(gameData);

                        foreach (var particle in gameData.particles)
                        {
                            particle.Draw(gameData.camera);
                        }
                        // Draw Player
                        gameData.player.Draw(gameData.camera);

                        //Draw entities
                        {
                            foreach (var entity in gameData.entities)
                                entity.Draw(gameData.camera);

                            foreach (var particle in gameData.particles)
                                particle.Draw(gameData.camera);
                        }

                        for (int i = 0; i < gameData.player.Life; i++)
                            Graphics.DrawPlayerOverlay(new Vector2(GetScreenWidth() / 2 - 125 + 50 * i, 85), 90);

                        DrawText("Score", 200, 50, 50, Color.GREEN);
                        DrawText($"{gameData.Score}", 200, 100, 50, Color.GREEN);
                        DrawText("Highscore", GetScreenWidth() - MeasureText("Highscore", 50) / 2 - 200, 50, 50, Color.GREEN);
                        DrawText($"{gameData.highscore}", GetScreenWidth() - 320, 100, 50, Color.GREEN);

                        Graphics.DrawCursor(inputs.ScreenPos + inputs.ShootTarget);

                        DrawFPS(10, 10);
                    }
                    break;
                case GameData.Scene.SETTINGS:
                    {
                        if (timeCount > 0)
                            DrawText("You cannot assign a key that is already set", GetScreenWidth() / 2 - MeasureText("You cannot assign a key that is already set", 75) / 2, 20, 75, Color.RED);
                    }
                    break;
                case GameData.Scene.GAME_OVER:
                    {
                        DrawText($"Score : {gameData.Score}", GetScreenWidth() / 2 - MeasureText($"Score : {gameData.Score}", 50) / 2, 600, 50, Color.BLUE);
                        DrawText($"High Score : {gameData.highscore}", GetScreenWidth() / 2 - MeasureText($"High Score : {gameData.highscore}", 100) / 2, 700, 100, Color.GREEN);
                    }
                    break;
                default:
                    break;
            }
            gameData.ui.Draw(gameData.scene, config);
        }

        public bool IsInside(Vector2 pos)
        {
            if ((pos.X < GetScreenWidth() && pos.X > 0) && (pos.Y < GetScreenHeight() && pos.Y > 0))
                return true;
            else
                return false;
        }

        public void LoadConfigFile()
        {
            config.LoadConfigFile();
        }

        public void WriteConfigFile()
        {
            config.WriteConfigFile();
        }
    }
}
