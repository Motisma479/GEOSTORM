using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities;
using Geostorm.Core.Entities.Enemies;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using System.Numerics;
namespace Geostorm.Core
{
    class GameData
    {
        public enum Scene 
        {
            MainMenu,
            InGame,
            Pause,
        }

        public Scene scene;
        public Ui ui;

        public Vector2 MapSize;
        private Player player = new Player();
        public IEnumerable<Entity> Entities { get { return entities; } }
        public Player Player { get { return player; } }
        public IEnumerable<Bullet> Bullets { get { return bullets; } }
        public IEnumerable<BlackHole> BlackHoles { get { return blackHoles; } }

        public List<Enemy> enemies = new List<Enemy>();
        public List<Entity> entities = new List<Entity>();
        public List<Bullet> bullets = new List<Bullet>();
        public List<BlackHole> blackHoles = new List<BlackHole>();
        public List<Star> stars = new List<Star>();
        public Camera camera = new Camera();


        // Temporary List
        private List<Enemy> enemiesAdded = new List<Enemy>();
        private List<Bullet> bulletsAdded = new List<Bullet>();
        private List<BlackHole> blackHoleAdded = new List<BlackHole>();

        public GameData()
        {
            ui = new Ui(scene);
            scene = Scene.MainMenu;
            MapSize = new Vector2(2000,2000);
            for (int i = 0; i < 5000; i++)
                stars.Add(new Star(new Vector2(GetRandomValue(-2500, 2500), GetRandomValue(-2500, 2500)), GetRandomValue(1, 4)));
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
        }
    }
}
