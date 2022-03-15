using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using Geostorm.Core;

namespace Geostorm.Renderer.Particles
{
    class Fire : Particle
    {
        public Fire(Vector2 position, float rotation, Color color)
        {
            time = 60;
            Position = position;
            Rotation = rotation;
            Color = color;
            Velocity = MathHelper.GetVectorRot(Rotation);
        }

        public override void Update(GameData data)
        {
            Position += Velocity;
            if (!Raylib.CheckCollisionPointRec(Position, new Rectangle(CollisionRadius * 2, CollisionRadius * 2, data.MapSize.X - CollisionRadius * 4, data.MapSize.Y - CollisionRadius * 4)))
            {
                time = 0;
            }
            time--;
        }
    }

}
