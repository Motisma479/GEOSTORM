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
        public Vector2 ScreenSize;
        public float DeltaTime;
        public Vector2 MoveAxis;
        public Vector2 ShootAxis;
        public bool Shoot;
        public Vector2 ShootTarget;

        public void Update(GameConfig configs)
        {
            ScreenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
            DeltaTime = GetFrameTime();
            MoveAxis = new Vector2(
            configs.KeyboardInputs[3].ReadAxisKey() - configs.KeyboardInputs[1].ReadAxisKey(),
            configs.KeyboardInputs[2].ReadAxisKey() - configs.KeyboardInputs[0].ReadAxisKey()
            );
            ShootTarget = (GetMousePosition()- ScreenPos);
            Shoot = configs.KeyboardInputs[4].ReadButtonKey();
            if (MoveAxis.Length() > 1) MoveAxis /= MoveAxis.Length();

        }
    }
}
