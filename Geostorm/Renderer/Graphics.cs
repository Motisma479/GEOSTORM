using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm.Renderer
{
    class Graphics
    {
        public void Load() { }
        public void Unload() { }

        public void DrawPlayer(Vector2 pos, float rotation) { }
        public void DrawGrunt(Vector2 pos, float activeTime) { }
        public void DrawBullet(Vector2 pos, float rotation) { }
    }
}
