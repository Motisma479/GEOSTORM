using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities;
using Geostorm.Core.Entities.Enemies;
using Geostorm.Renderer.Particles;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using System.Numerics;
using System.IO;

namespace Geostorm.Core
{
    class GameData
    {
        public enum Scene 
        {
            MAIN_MENU,
            IN_GAME,
            PAUSE,
            SETTINGS,
            GAME_OVER
        }
        public int round = 0;
        public int highscore;

        public Scene scene;
        public Ui ui;
        public Grid grid;

        public List<Particle> particles = new List<Particle>();

        public Vector2 MapSize;
        public Player player = new Player();
        public IEnumerable<Entity> Entities { get { return entities; } }
        public IEnumerable<Bullet> Bullets { get { return bullets; } }
        public IEnumerable<BlackHole> BlackHoles { get { return blackHoles; } }

        public List<Enemy> enemies = new List<Enemy>();
        public List<Entity> entities = new List<Entity>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<BlackHole> blackHoles = new List<BlackHole>();
        public List<Star> stars = new List<Star>();
        public Camera camera = new Camera();

        public float DeltaTime = 0;
        public float TotalTime = 0;
        public Random rng;

        public int Score;

        // Temporary List
        private List<Enemy> enemiesAdded = new List<Enemy>();
        private List<Bullet> bulletsAdded = new List<Bullet>();
        private List<BlackHole> blackHoleAdded = new List<BlackHole>();

        public GameData()
        {
            rng = new Random();
            scene = Scene.MAIN_MENU;
            MapSize = new Vector2(350 * 4, 350 * 3);
            for (int i = 0; i < 1400; i++)
            {
                stars.Add(new Star(new Vector2(GetRandomValue(-500, (int)(MapSize.X + 500)), GetRandomValue(-500, (int)(MapSize.Y + 500))), GetRandomValue(1, 4)));
            }
            grid = new Grid(MapSize);
            ReadHighscore();
            ui = new Ui(Scene.MAIN_MENU, ref scene, this);
        }

        public void UpdateDeltaTime()
        {
            DeltaTime = GetFrameTime();
            TotalTime = (float)GetTime();
        }

        public void AddEnemyDelayed(Enemy enemy) 
        {
            enemiesAdded.Add(enemy);
        }
        public void AddBulletDelayed(Bullet bullet) 
        {
            bulletsAdded.Add(bullet);
        }
        public void AddBlackHoleDelayed(BlackHole blackHole) 
        { 
            blackHoleAdded.Add(blackHole);
        }

        public void Synchronize()
        {
            entities.AddRange(bulletsAdded);
            bullets.AddRange(bulletsAdded);

            bulletsAdded.Clear();
            bullets.RemoveAll(Entity => Entity.IsDead);
            entities.AddRange(blackHoleAdded);
            blackHoles.AddRange(blackHoleAdded);
            blackHoleAdded.Clear();
            blackHoles.RemoveAll(Entity => Entity.IsDead);
            entities.AddRange(enemiesAdded);
            enemies.AddRange(enemiesAdded);
            enemiesAdded.Clear();
            enemies.RemoveAll(Entity => Entity.IsDead);
            entities.RemoveAll(Entity => Entity.IsDead);
        }

        public void InitGameData()
        {
            entities.Clear();
            bullets.Clear();
            enemies.Clear();
            particles.Clear();
            blackHoles.Clear();

            grid.Initialize();

            round = 0;
            player.WeaponLevel = 0;
            player.ResetWeapon();
            Score = 0;
            particles.Clear();
            ChangeRound();
        }

        public void ChangeRound()
        {
            round++;
            for (int i = 0; i < round * 2; i++)
                AddEnemyDelayed(new Grunt(90 + rng.Next(0,round*50+100), this));
            for (int i = 0; i < round; i++)
                AddEnemyDelayed(new Mill(90 + rng.Next(0, round * 50 + 100), this));
            for (int i = 0; i < rng.Next(1,(round > 2 ? 4 : 3)); i++)
                AddBlackHoleDelayed(new BlackHole(new Vector2(rng.Next(100, (int)(MapSize.X - 100)), rng.Next(100, (int)(MapSize.Y - 100))), GetRandomValue(35, 50)));
            for (int i = 0; i < Grid.Length; ++i)
                Grid[i].ReinitGrid();
            Synchronize();
        }

        public void ReadHighscore()
        {
            var installDirectory = AppContext.BaseDirectory;
            string[] inputs = File.ReadAllLines(installDirectory + "highscore.txt");
            this.highscore = Int32.Parse(inputs[0]);
        }

        public void WriteHighscore(int x)
        {
            var installDirectory = AppContext.BaseDirectory;
            string[] txt = new string[1];
            txt[0] = (x.ToString());
            File.WriteAllLines(installDirectory + "highscore.txt", txt);
        }
    }
}
