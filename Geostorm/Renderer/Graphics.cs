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

        public void DrawStar(Vector2 pos)
        {
            DrawCircle((int)pos.X, (int)pos.Y, 1, Color.WHITE);
        }

        public void DrawPointer(Vector2 pos, float rotation)
        {
            //DrawCircleV(pos, 3.0f, Color.WHITE);
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation + 90)));
            for (int i = 0; i <= Core.Entities.PointerTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.PointerTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.PointerTexture.Points[(i + 1) % Core.Entities.PointerTexture.Points.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.PointerTexture.thickness / 2, Color.WHITE);
                DrawLineEx(curentP, curentP2, Core.Entities.PointerTexture.thickness, Color.WHITE);

            }
        }

        public void DrawCursor(Vector2 pos)
        {
            Vector2 newPos = new Vector2(pos.X - Core.Entities.CurssorTexture.preScale / 4, pos.Y - Core.Entities.CurssorTexture.preScale / 1.3f);
            DrawLineEx(Core.Entities.CurssorTexture.Cross[0] + newPos, Core.Entities.CurssorTexture.Cross[1] + newPos, Core.Entities.CurssorTexture.thickness, Color.GREEN);
            DrawLineEx(Core.Entities.CurssorTexture.Cross[2] + newPos, Core.Entities.CurssorTexture.Cross[3] + newPos, Core.Entities.CurssorTexture.thickness, Color.GREEN);
            for (int i = 0; i <= Core.Entities.CurssorTexture.Points.Length - 1; i++)
            {
                DrawCircleV(Core.Entities.CurssorTexture.Points[i] + newPos, Core.Entities.CurssorTexture.thickness / 2, Color.DARKGREEN);
                DrawLineEx(Core.Entities.CurssorTexture.Points[i] + newPos, Core.Entities.CurssorTexture.Points[(i + 1) % Core.Entities.CurssorTexture.Points.Length] + newPos, Core.Entities.CurssorTexture.thickness, Color.DARKGREEN);
            }

        }

        public void DrawPlayer(Vector2 pos, float rotation, float weaponRotation)
        {
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.PlayerTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.PlayerTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.PlayerTexture.Points[(i + 1) % Core.Entities.PlayerTexture.Points.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.PlayerTexture.thickness / 2, Color.WHITE);
                DrawLineEx(curentP, curentP2, Core.Entities.PlayerTexture.thickness, Color.WHITE);

            }
            DrawPointer(pos + MathHelper.GetVectorRot(weaponRotation) * 23, weaponRotation);
        }
        public void DrawGrunt(Vector2 pos, float rotation, float activeTime)
        {
            //Copie of the other but d
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.GruntTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.GruntTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.GruntTexture.Points[(i + 1) % Core.Entities.GruntTexture.Points.Length], rotate) + pos;

                DrawLineEx(curentP, curentP2, Core.Entities.GruntTexture.thickness, Color.WHITE);

            }
        }

        public void DrawBullet(Vector2 pos, float rotation)
        {
            Matrix3x2 rotate = Matrix3x2.CreateRotation((rotation * MathF.PI / 180));
            for (int i = 0; i <= Core.Entities.BulletTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.BulletTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.BulletTexture.Points[(i + 1) % Core.Entities.BulletTexture.Points.Length], rotate) + pos;

                DrawLineEx(curentP, curentP2, Core.Entities.BulletTexture.thickness, Color.WHITE);

            }
        }

        public void DrawGridLine(Vector2 posA, Vector2 posB, Rectangle size)
        {
            if (CheckCollisionPointRec(posA, size) && CheckCollisionPointRec(posB, size))
                DrawLineEx(posA, posB, 1, new Color(20, 105, 253, 60 * 255 / 100));
        }
    }
}
