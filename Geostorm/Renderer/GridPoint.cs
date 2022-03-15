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
            float pLength = (data.Player.Position - pPos).Length();
            if (pLength < data.Player.Range)
            {
                pVel += (data.Player.Position - pPos) / data.Player.Weight * -MathF.Pow(pLength - data.Player.Range, 2);
            }
            foreach (var item in data.entities)
            {
                if (item.IsDead) continue;
                float mLength = (item.Position - pPos).Length();
                if (mLength < item.Range)
                {
                    pVel += (item.Position - pPos) / item.Weight * -MathF.Pow(mLength - item.Range, 2);
                }
            }
            for (int i = 0; i < connexions.Length; i++)
            {
                float cLength = (connexions[i].pPos - pPos).Length();
                if (cLength > 25)
                {
                    pVel += (connexions[i].pPos - pPos) / 1250 * (cLength - 25);
                }
            }
        }

        public void UpdatePos()
        {
            pVel = new Vector2(MathHelper.CutFloat(pVel.X, -40, 40), MathHelper.CutFloat(pVel.Y, -40, 40));
            pVel = pVel * 0.95f;
            if (!fixedPoint) pPos = pPos + pVel;
        }

        public void Draw(Graphics graphics, Camera camera, Vector2 size)
        {
            for (int i = 0; i < connexions.Length; i++)
            {
                graphics.DrawGridLine((connexions[i].pPos + pPos) / 2 + camera.Pos, pPos + camera.Pos, new Raylib_cs.Rectangle(camera.Pos.X, camera.Pos.Y, size.X, size.Y));
            }
        }
    }
}
