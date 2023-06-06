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
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity = new Vector2();
        public float Rotation;
        public Color Color;
        public float time;
        public float CollisionRadius = 3;

        public virtual void Update(GameData data)
        {

        }

        public virtual void Draw(Camera cam)
        {
            Graphics.DrawParticle(Position + cam.Pos, Rotation, Color, time);
        }
    }
}
