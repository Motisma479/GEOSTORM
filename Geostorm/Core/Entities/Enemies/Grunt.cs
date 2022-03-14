using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities.Enemies
{
    class Grunt : Enemy
    {
        public int spawnTime;
        public Grunt(int spawnTime)
        {
            this.spawnTime = spawnTime;
        }
        Vector2 pos;
        public Vector2 Pos { get { return pos; } set { pos = value; } }
        float rotation;
        
        public void DoUpdate() { }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawGrunt(pos, rotation, spawnTime);
        }
    }
}
