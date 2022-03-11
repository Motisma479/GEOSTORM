using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Geostorm.Renderer
{
    class Graphics
    {
        Texture2D test;
        public void Load()
        {
            var installDirectory = AppContext.BaseDirectory;
            test = LoadTexture(installDirectory + "Assets/SUS_4.png");
        }
        public void Unload()
        {
            UnloadTexture(test);
        }

        public void DrawPlayer(Vector2 pos, float rotation)
        {
            DrawTextureEx(test,pos,rotation,5.0f,Color.WHITE);
        }
        public void DrawGrunt(Vector2 pos, float activeTime) { }
        public void DrawBullet(Vector2 pos, float rotation) { }
    }
}
