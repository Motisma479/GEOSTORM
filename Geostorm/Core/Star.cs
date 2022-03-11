using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static Raylib_cs.Raylib;
using Raylib_cs;
using Geostorm.Renderer;

namespace Geostorm.Core
{
    class Star
    {
        public Vector2 Pos;
        int Depth;
        public float Speed = 1;

        public Star(Vector2 pos, int depth)
        {
            this.Depth = depth;
            this.Pos = pos;
            switch (Depth)
            {
                case 0:
                    Speed = 1;
                    break;
                case 1:
                    Speed = 0.7f;
                    break;
                case 2:
                    Speed = 0.5f;
                    break;
                case 3:
                    Speed = 0.25f;
                    break;
                default:
                    break;
            }
        }
        public void Update(Camera cam) 
        {

        }

        public void Draw(Graphics graphics, Camera cam)
        {
            graphics.DrawStar(Pos + cam.Pos * Speed);
        }
    }
}
