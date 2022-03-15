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
namespace Geostorm.Core
{
    class GameData
    {
        public enum Scene 
        {
            MAIN_MENU,
            IN_GAME,
            PAUSE,
            SETTINGS
        }
        public int round = 0;

        public Scene scene;
        public Ui ui;
        public GridPoint[] Grid;

        public List<Particle> particles = new List<Particle>();

        public Vector2 MapSize;
        private Player player = new Player();
        public IEnumerable<Entity> Entities { get { return entities; } }
        public Player Player { get { return player; } set { player = value; } }
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
            int square = 25;
            int width = (int)MapSize.X / square + 1;
            int height = (int)MapSize.Y / square + 1;
            Grid = new GridPoint[width*height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Grid[j * width + i] = new GridPoint(new Vector2(i * square, j * square), (i == 0 || i == width - 1 || j == 0 || j == height - 1));
                }
            }
            for (int i = 0; i < width * height; i++)
            {
                GridPoint[] connect = new GridPoint[4];
                int count = 0;
                for (int j = 0; j < 4; j++)
                {
                    Vector2 pos = new Vector2(i % width, i / width) + Directions.Dir[j];
                    if (pos.X >= 0 && pos.X < width && pos.Y >= 0 && pos.Y < height)
                    {
                        connect[count] = Grid[(int)pos.X + (int)pos.Y * width];
                        count++;
                    }
                }
                Grid[i].AddPoints(connect, count);
            }
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
            int square = 25;
            int width = (int)MapSize.X / square + 1;
            int height = (int)MapSize.Y / square + 1;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Grid[j * width + i].pPos = new Vector2(i * square, j * square);
                    Grid[j * width + i].pVel = new Vector2();
                }
            }
            round = 0;
            Player.WeaponLevel = 0;
            Player.ResetWeapon();
            Score = 0;
            particles.Clear();
            ChangeRound();
        }

        public void ChangeRound()
        {
            enemies.Clear();
            entities.Clear();
            blackHoles.Clear();
            round++;
            for (int i = 0; i < round * 2; i++)
                AddEnemyDelayed(new Core.Entities.Enemies.Grunt(90 + rng.Next(0,round*50+100), this));
            for (int i = 0; i < rng.Next(2,3); i++)
                AddBlackHoleDelayed(new BlackHole(new Vector2(rng.Next(100, (int)(MapSize.X - 100)), rng.Next(100, (int)(MapSize.Y - 100))), GetRandomValue(35, 50)));
            Synchronize();
        }
    }
}
