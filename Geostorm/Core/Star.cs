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
        int Speed;

        public Star(Vector2 pos, int depth)
        {
            this.Depth = depth;
            this.Pos = pos;
            switch (Depth)
            {
                case 0:
                    Speed = 5;
                    break;
                case 1:
                    Speed = 3;
                    break;
                case 2:
                    Speed = 1;
                    break;
                default:
                    break;
            }
        }
        public void Update(Camera cam) 
        {

        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawStar(Pos);
        }
    }
}
