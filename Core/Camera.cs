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
            pos = new Vector2(500,500);
        }

        public void Update(GameInputs inputs, Vector2 position, Vector2 mapSize)
        {
            pos = inputs.ScreenSize/2 - mapSize/2 + (mapSize / 2 - position)*0.5f;
            inputs.ScreenPos = pos;
        }
    }
}
