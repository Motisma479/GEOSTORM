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
        public Vector2 position;
        private int depth;
        public float speed = 1;

        public Star(Vector2 pos, int _depth)
        {
            depth = _depth;
            position = pos;
            switch (depth)
            {
                default:
                case 0:
                    speed = 0.6f;
                    break;
                case 1:
                    speed = 0.5f;
                    break;
                case 2:
                    speed = 0.3f;
                    break;
                case 3:
                    speed = 0.25f;
                    break;
            }
        }

        public void Update(Camera cam) 
        {

        }

        public void Draw(Camera cam)
        {
            Graphics.DrawStar(position + cam.Pos * speed);
        }
    }
}
