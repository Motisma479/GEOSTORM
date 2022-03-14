using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
using Geostorm.Core;

namespace Geostorm.Renderer
{
    class GridPoint
    {
        public Vector2 pVel;
        public Vector2 pPos;
        readonly bool fixedPoint;
        public GridPoint[] connexions;
        int numberOfUpdate;

        public GridPoint(Vector2 position, bool isFixed)
        {
            pVel = new Vector2(0);
            pPos = position;
            fixedPoint = isFixed;
        }
        public GridPoint() : this(new Vector2(), true) { }

        public void AddPoints(GridPoint[] points, int count)
        {
            connexions = new GridPoint[count];
            for (int i = 0; i < count; i++)
            {
                connexions[i] = points[i];
            }
        }

        public void UpdatePoint(GameData data)
        {
            float mLength;
            numberOfUpdate = data.bullets.Count() + 1;
            int j = 0;
            do
            {
                if (data.bullets.Count() > 0)
                    mLength = (data.bullets[j].Position - pPos).Length();
                else
                    mLength = (data.Player.Position - pPos).Length();
                if (mLength < 25)
                {
                    pVel += (data.Player.Position - pPos) / 100000000 * -MathF.Pow(mLength - 400, 2) * 2.0f;
                }
                for (int i = 0; i < connexions.Length; i++)
                {
                    float pLength = (connexions[i].pPos - pPos).Length();
                    if (pLength > 22)
                    {
                        pVel = pVel + (connexions[i].pPos - pPos) / 10000 * (pLength - 22) * 32;
                    }
                }
                j++;
            }
            while (j < data.bullets.Count());
        }

        public void UpdatePos()
        {
                pVel = new Vector2(MathHelper.CutFloat(pVel.X, -40, 40), MathHelper.CutFloat(pVel.Y, -40, 40));
                pVel = pVel * 0.95f;
                if (!fixedPoint) pPos = pPos + pVel;
        }

        public void Draw(Graphics graphics, Camera camera)
        {
            for (int i = 0; i < connexions.Length; i++)
            {
                graphics.DrawGridLine((connexions[i].pPos + pPos) / 2 + camera.Pos, pPos + camera.Pos);
            }
        }
    }
}
