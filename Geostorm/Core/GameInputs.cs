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
        public Vector2 LocalSize;
        public float DeltaTime;
        public Vector2 MoveAxis;
        public Vector2 ShootAxis;
        public bool Shoot;
        public Vector2 ShootTarget = new Vector2(100,100);

        public void Update(GameConfig configs, Vector2 playerPos)
        {
            ScreenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
            DeltaTime = GetFrameTime();
            MoveAxis = new Vector2(
            configs.KeyboardInputs[3].ReadAxisKey() - configs.KeyboardInputs[1].ReadAxisKey(),
            configs.KeyboardInputs[2].ReadAxisKey() - configs.KeyboardInputs[0].ReadAxisKey()
            );
            switch (configs.AimType)
            {
                case 1:
                    ShootAxis = new Vector2(
                    configs.KeyboardInputs[8].ReadAxisKey() - configs.KeyboardInputs[6].ReadAxisKey(),
                    configs.KeyboardInputs[7].ReadAxisKey() - configs.KeyboardInputs[5].ReadAxisKey()
                    );
                    if (ShootAxis.Length() < 0.1f) break;
                    if (ShootAxis.Length() > 1) ShootAxis /= ShootAxis.Length();
                    ShootTarget += ShootAxis * 17;
                    ShootTarget = new Vector2(MathHelper.CutFloat(ShootTarget.X,0,LocalSize.X), MathHelper.CutFloat(ShootTarget.Y, 0, LocalSize.Y));
                    break;
                case 2:
                    ShootAxis = new Vector2(
                    configs.KeyboardInputs[8].ReadAxisKey() - configs.KeyboardInputs[6].ReadAxisKey(),
                    configs.KeyboardInputs[7].ReadAxisKey() - configs.KeyboardInputs[5].ReadAxisKey()
                    );
                    if (ShootAxis.Length() < 0.1f) ShootTarget = playerPos;
                    else
                    {
                        ShootTarget = playerPos + (ShootAxis / ShootAxis.Length())*30;
                    }
                    break;
                default:
                    ShootTarget = (GetMousePosition() - ScreenPos);
                    break;
            }
            Shoot = configs.KeyboardInputs[4].ReadButtonKey();
            if (MoveAxis.Length() > 1) MoveAxis /= MoveAxis.Length();

        }
    }
}
