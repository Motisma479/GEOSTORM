using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities
{
    class Bullet : Entity
    {
        public Bullet()
        {

        }
        public void Update() 
        {
        }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawBullet(Position, Rotation);
        }
    }
}
