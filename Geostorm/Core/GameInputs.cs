using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Geostorm.Core
{
    class GameInputs
    {
        public Vector2 ScreenPos;
        public float DeltaTime;
        public Vector2 MoveAxis;
        public Vector2 ShootAxis;
        public bool Shoot;
        public Vector2 ShootTarget;

        private int boolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public void Update(GameConfig configs)
        {
            DeltaTime = GetFrameTime();
            MoveAxis = new Vector2(
            boolToInt(IsKeyDown((Raylib_cs.KeyboardKey)configs.KeyboardInputs[3])) - boolToInt(IsKeyDown((Raylib_cs.KeyboardKey)configs.KeyboardInputs[1])),
            boolToInt(IsKeyDown((Raylib_cs.KeyboardKey)configs.KeyboardInputs[2])) - boolToInt(IsKeyDown((Raylib_cs.KeyboardKey)configs.KeyboardInputs[0]))
            );
            ShootTarget = (GetMousePosition()- ScreenPos);
            Shoot = IsKeyDown((Raylib_cs.KeyboardKey)configs.KeyboardInputs[4]);
            if (MoveAxis.Length() > 1) MoveAxis /= MoveAxis.Length();

        }
    }
}
