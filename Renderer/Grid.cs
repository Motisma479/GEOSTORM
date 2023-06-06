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
    class Grid
    {
        private GridPoint[] points;

        private int width;
        private int height;
        private const int square = 25;

        public Grid(Vector2 size)
        {
            int square = 25;

            width = (int)size.X / square + 1;
            height = (int)size.Y / square + 1;

            points = new GridPoint[width * height];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    points[j * width + i] = new GridPoint(new Vector2(i * square, j * square), (i == 0 || i == width - 1 || j == 0 || j == height - 1));
                }
            }
            for (int i = 0; i < width * height; i++)
            {
                GridPoint[] connect = new GridPoint[4];
                int count = 0;
                for (int j = 0; j < 4; j++)
                {
                    Vector2 pos = new Vector2(i % width, i / width) + Core.Directions.Dir[j];
                    if (pos.X >= 0 && pos.X < width && pos.Y >= 0 && pos.Y < height)
                    {
                        connect[count] = points[(int)pos.X + (int)pos.Y * width];
                        count++;
                    }
                }
                points[i].AddPoints(connect, count);
            }

        }

        public void Initialize()
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    points[j * width + i].pPos = new Vector2(i * square, j * square);
                    points[j * width + i].pVel = new Vector2();
                }
            }
        }

        public void Update(GameData gameData)
        {
            foreach (var item in points)
            {
                item.UpdatePoint(gameData);
            }
            foreach (var item in points)
            {
                item.UpdatePos();
            }
        }

        public void Draw(GameData gameData)
        {
            foreach (var point in points)
                point.Draw(gameData.camera, gameData.MapSize);

            DrawRectangleLinesEx(new Rectangle(gameData.camera.Pos.X, gameData.camera.Pos.Y, gameData.MapSize.X, gameData.MapSize.Y), 5, Color.WHITE);
        }
    }
}
