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
            }
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            graphics.DrawBullet(Position + camera.Pos, Rotation);
            //Raylib_cs.Raylib.DrawCircleLines((int)(Position.X + camera.Pos.X), (int)(Position.Y + camera.Pos.Y), CollisionRadius, Raylib_cs.Color.RED);
        }
    }
}
