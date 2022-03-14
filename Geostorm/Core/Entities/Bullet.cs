using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;
using Raylib_cs;

namespace Geostorm.Core.Entities
{
    class Bullet : Entity
    {
        public Bullet()
        {
            CollisionRadius = 5;
        }
        public void Update(GameData data)
        {
            Position += Velocity;
            if (!Raylib.CheckCollisionPointRec(Position, new Rectangle(CollisionRadius * 2, CollisionRadius * 2, data.MapSize.X - CollisionRadius * 4, data.MapSize.Y - CollisionRadius * 4)))
            {
                IsDead = true;
                for (int i = 0; i < Raylib.GetRandomValue(10,20); i++)
                    data.particles.Add(new Particle(Position, i * Raylib.GetRandomValue(0, 360), Color.YELLOW));
            }
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            graphics.DrawBullet(Position + camera.Pos, Rotation);
        }
    }
}
