using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm.Core
{
    class GameInputs
    {
        public Vector2 ScreenSize;
        public float DeltaTime;
        public Vector2 MoveAxis;
        public Vector2 ShootAxis;
        public bool Shoot;
        public Vector2 ShootTarget;
    }
}
