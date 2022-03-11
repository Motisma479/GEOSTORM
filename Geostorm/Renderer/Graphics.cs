using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using Geostorm.Core;

namespace Geostorm.Renderer
{
    class Graphics
    {
        List<Star> Stars = new List<Star>();
        public Graphics()
        {
            Load();
        }
        ~Graphics()
        {
            Unload();
        }
        public void Load()
        {
            var installDirectory = AppContext.BaseDirectory;
            //test = LoadTexture(installDirectory + "Assets/SUS_4.png");
        }
        public void Unload()
        {
        }
        public void DrawMap(Vector2 size, Vector2 pos)
        {
            const int square = 16;
            int width = (int)size.X/ square;
            int height = (int)size.Y/ square;
            
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    DrawRectangleLinesEx(new Rectangle( pos.X + i* square, pos.Y + j* square, square, square), 1, new Color(20, 105, 253, 60*255/100));
                }
            }
            
            DrawRectangleLinesEx(new Rectangle(pos.X,pos.Y,size.X, size.Y), 5, Color.WHITE);
        }
        public void DrawStar(Vector2 pos)
        {
            DrawCircle((int)pos.X, (int)pos.Y, 1, Color.WHITE);
        }

        public void DrawPlayer(Vector2 pos, float rotation) 
        {
        }
        public void DrawGrunt(Vector2 pos, float activeTime) 
        {
        }
        public void DrawBullet(Vector2 pos, float rotation) 
        {
        }
    }
}
