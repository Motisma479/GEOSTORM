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
            Vector2 newPos = new Vector2(pos.X - Core.Entities.CursorTexture.preScale / 4, pos.Y - Core.Entities.CursorTexture.preScale / 1.3f);
            DrawLineEx(Core.Entities.CursorTexture.Cross[0] + newPos, Core.Entities.CursorTexture.Cross[1] + newPos, Core.Entities.CursorTexture.thickness, Color.GREEN);
            DrawLineEx(Core.Entities.CursorTexture.Cross[2] + newPos, Core.Entities.CursorTexture.Cross[3] + newPos, Core.Entities.CursorTexture.thickness, Color.GREEN);
            for (int i = 0; i <= Core.Entities.CursorTexture.Points.Length - 1; i++)
            {
                DrawCircleV(Core.Entities.CursorTexture.Points[i] + newPos, Core.Entities.CursorTexture.thickness / 2, Color.DARKGREEN);
                DrawLineEx(Core.Entities.CursorTexture.Points[i] + newPos, Core.Entities.CursorTexture.Points[(i + 1) % Core.Entities.CursorTexture.Points.Length] + newPos, Core.Entities.CursorTexture.thickness, Color.DARKGREEN);
            }

        }
        public void DrawTurret(Vector2 pos, float rotation)
        {
            //DrawCircleV(pos, 3.0f, Color.WHITE);
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.TuretTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.TuretTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.TuretTexture.Points[(i + 1) % Core.Entities.TuretTexture.Points.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.TuretTexture.thickness / 2, Color.WHITE);
                DrawLineEx(curentP, curentP2, Core.Entities.TuretTexture.thickness, Color.WHITE);

            }
        }
        public void DrawPlayer(Vector2 pos, float rotation, float weaponRotation, UInt32 turretNumber = 0)
        {
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.PlayerTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.PlayerTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.PlayerTexture.Points[(i + 1) % Core.Entities.PlayerTexture.Points.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.PlayerTexture.thickness / 2, Color.WHITE);
                DrawLineEx(curentP, curentP2, Core.Entities.PlayerTexture.thickness, Color.WHITE);

            }

            Vector2 delta = MathHelper.GetVectorRot(weaponRotation);
            Vector2 norm = new Vector2(-delta.Y, delta.X);
            if(turretNumber %2 ==0)
            {
                for (int i = 0; i < turretNumber; i++)
                {
                    DrawTurret(pos + (i % 2 == 0 ? delta + (norm * (i + 1)) : delta - (norm * i)) * 23, weaponRotation);

                }
            }
            else
            {
                for (int i = 0; i < turretNumber; i++)
                {
                    DrawTurret(pos + (i % 2 == 0 ? delta + (norm * (i)) : delta - (norm * (i+1))) * 23, weaponRotation);

                }
            }
            
            DrawPointer(pos + delta * 23, weaponRotation);
        }
        public void DrawPlayerOverlay(Vector2 pos, float rotation)
        {
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.PlayerTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.PlayerTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.PlayerTexture.Points[(i + 1) % Core.Entities.PlayerTexture.Points.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.PlayerTexture.thickness / 2, Color.GREEN);
                DrawLineEx(curentP, curentP2, Core.Entities.PlayerTexture.thickness, Color.GREEN);

            }
        }
        public void DrawGrunt(Vector2 pos, Vector2 renderScale, float rotation, float activeTime)
        {
            Color renderColor = Color.BLUE;
            if (activeTime > 60) return;
            if (activeTime > 0)
            {
                renderScale = new Vector2(1 + ((activeTime % 10) / 10) * 2);
                renderColor = Fade(Color.BLUE,1-activeTime/60);
            }
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.GruntTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.GruntTexture.Points[i], rotate) * renderScale + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.GruntTexture.Points[(i + 1) % Core.Entities.GruntTexture.Points.Length], rotate) * renderScale + pos;

                DrawCircleV(curentP, Core.Entities.GruntTexture.thickness / 2, renderColor);
                DrawLineEx(curentP, curentP2, Core.Entities.GruntTexture.thickness, renderColor);

            }
        }
        public void DrawMill(Vector2 pos, float rotation, float activeTime)
        {
            Color renderColor = Color.PURPLE;
            if (activeTime > 60) return;
            if (activeTime > 0)
            {
                rotation = activeTime * 10;
            }
            Matrix3x2 rotate = Matrix3x2.CreateRotation((MathHelper.ToRadians(rotation)));
            for (int i = 0; i <= Core.Entities.MillTexture.Pal1.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.MillTexture.Pal1[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.MillTexture.Pal1[(i + 1) % Core.Entities.MillTexture.Pal1.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.MillTexture.thickness / 2, renderColor);
                DrawLineEx(curentP, curentP2, Core.Entities.MillTexture.thickness, renderColor);

            }
            for (int i = 0; i <= Core.Entities.MillTexture.Pal2.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.MillTexture.Pal2[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.MillTexture.Pal2[(i + 1) % Core.Entities.MillTexture.Pal2.Length], rotate) + pos;

                DrawCircleV(curentP, Core.Entities.MillTexture.thickness / 2, renderColor);
                DrawLineEx(curentP, curentP2, Core.Entities.MillTexture.thickness, renderColor);

            }
        }

        public void DrawBullet(Vector2 pos, float rotation)
        {
            Matrix3x2 rotate = Matrix3x2.CreateRotation((rotation * MathF.PI / 180));
            for (int i = 0; i <= Core.Entities.BulletTexture.Points.Length - 1; i++)
            {
                Vector2 curentP = Vector2.Transform(Core.Entities.BulletTexture.Points[i], rotate) + pos;
                Vector2 curentP2 = Vector2.Transform(Core.Entities.BulletTexture.Points[(i + 1) % Core.Entities.BulletTexture.Points.Length], rotate) + pos;

                DrawLineEx(curentP, curentP2, Core.Entities.BulletTexture.thickness, Color.YELLOW);

            }
        }

        public void DrawGridLine(Vector2 posA, Vector2 posB, Rectangle size)
        {
            if (CheckCollisionPointRec(posA, size) && CheckCollisionPointRec(posB, size))
                DrawLineEx(posA, posB, 1, new Color(20, 105, 253, 60 * 255 / 100));
        }

        public void DrawParticle(Vector2 pos, float rot, Color color, float time)
        {
            Vector2 p1 = new Vector2(- 6, 0);
            Vector2 p2 = new Vector2((float)(-8 + time * 0.3), 0);
            Matrix3x2 rotate = Matrix3x2.CreateRotation(MathHelper.ToRadians(rot));
            Vector2 curentP = Vector2.Transform(p1, rotate) + pos;
            Vector2 curentP2 = Vector2.Transform(p2, rotate) + pos;

            DrawLineEx(curentP, curentP2, 2, color);
        }

        public void DrawScoreParticle(string text, Vector2 pos, Color color, float time)
        {
            DrawText(text, (int)pos.X, (int)pos.Y, (int)time, color);
        }

        public void DrawBlackHole(Vector2 pos, float radius)
        {
            for (int i = 0; i < 5; i++)
                DrawCircleLines((int)pos.X, (int)pos.Y, (float)(radius + i*0.5), Color.RED);
        }

    }
}
