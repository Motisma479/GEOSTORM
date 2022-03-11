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
        public float zoom;
        public Camera()
        {
            pos = new Vector2(500,500);
            zoom = 1;
        }

        public void Update(GameInputs inputs, Vector2 mapSize)
        {
            if (IsMouseButtonDown(Raylib_cs.MouseButton.MOUSE_LEFT_BUTTON))
            {
                pos.X = GetMouseX() - mapSize.X/2;
                pos.Y = GetMouseY() - mapSize.Y/2;
            }
            if (GetMouseWheelMove() == 1)
                zoom += 0.1f;
            else if (GetMouseWheelMove() == -1)
                zoom -= 0.1f;
            inputs.ScreenPos = pos;
        }
    }
}
