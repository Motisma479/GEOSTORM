using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities;
using Geostorm.Core.Entities.Enemies;

namespace Geostorm.Core
{
    class GameData
    {
        Game game;

        public IEnumerable<Entity> Entities;
        public Player Player;
        public IEnumerable<Bullet> Bullets;
        public IEnumerable<BlackHole> BlackHoles;
        
        Player player;
        List<Enemy> enemies;
        List<Entity> entities;
        List<Bullet> bullets;
        List<BlackHole> blackHoles;
        

        // Temporary List
        private List<Enemy> enemiesAdded;
        private List<Bullet> bulletsAdded;
        private List<BlackHole> blackHoleAdded;

        public void AddEnemyDelayed(Enemy enemy) { }
        public void AddBulletDelayed(Enemy enemy) { }
        public void AddBlackHoleDelayed(Enemy enemy) { }
        public void Update()
        {
        }

        public void Synchronize () { }
    }
}
