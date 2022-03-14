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
        public Particle(Vector2 position, float rotation, Color color)
        {
            time = 60;
            Position = position;
            Rotation = rotation;
            Color = color;
            Velocity = MathHelper.GetVectorRot(Rotation) * 10;
        }
        public void Update()
        {
            Position += Velocity;
            time--;
        }
        public void Draw(Graphics graphics, Camera cam)
        {
            graphics.DrawParticle(Position + cam.Pos, Rotation, Color);
        }
    }
}
