using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities;
using Geostorm.Core.Entities.Enemies;
using Geostorm.Renderer;

namespace Geostorm.Core
{
    class GameData
    {
        public Game game;

        public IEnumerable<Entity> Entities { get { return entities; } }
        public Player Player { get { return player; } }
        public IEnumerable<Bullet> Bullets { get { return bullets; } }
        public IEnumerable<BlackHole> BlackHoles { get { return blackHoles; } }

        Player player;
        List<Enemy> enemies = new List<Enemy>();
        List<Entity> entities = new List<Entity>();
        List<Bullet> bullets = new List<Bullet>();
        List<BlackHole> blackHoles = new List<BlackHole>();


        // Temporary List
        private List<Enemy> enemiesAdded = new List<Enemy>();
        private List<Bullet> bulletsAdded = new List<Bullet>();
        private List<BlackHole> blackHoleAdded = new List<BlackHole>();

        public GameData()
        {
            game = new Game();
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
