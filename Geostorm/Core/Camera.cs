using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Geostorm.Core
{
    class Camera
    {
        Vector2 pos;
        public Vector2 Pos { get { return pos; } set { pos = value; } }
        public Camera()
        {

        }

        public void Update(GameInputs inputs)
        {
            pos.X = GetScreenWidth()/2 - GetMouseX();
            pos.Y = GetScreenHeight()/2 - GetMouseY();
            inputs.ScreenPos = pos;
        }
    }
}
