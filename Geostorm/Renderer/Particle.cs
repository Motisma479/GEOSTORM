using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using Geostorm.Core;

namespace Geostorm.Renderer
{
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity = new Vector2();
        public float Rotation;
        public Color Color;
        public float time;
        public float CollisionRadius = 3;
        int coolDown;
        public Particle(Vector2 position, float rotation, Color color)
        {
            time = 60;
            Position = position;
            Rotation = rotation;
            Color = color;
            Velocity = MathHelper.GetVectorRot(Rotation) * 10;
        }
        public void Update(GameData data)
        {
            Position += Velocity;
            if (!Raylib.CheckCollisionPointRec(Position, new Rectangle(CollisionRadius * 2, CollisionRadius * 2, data.MapSize.X - CollisionRadius * 4, data.MapSize.Y - CollisionRadius * 4)))
            {
                time = 0;
            }
            time--;
        }
        public void Draw(Graphics graphics, Camera cam)
        {
            graphics.DrawParticle(Position + cam.Pos, Rotation, Color);
        }
    }
}
