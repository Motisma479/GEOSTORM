using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using Geostorm.Renderer;
using Geostorm.Renderer.Particles;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace Geostorm.Core.Entities
{
    class BlackHole : Entity
    {
        bool active = false;
        public BlackHole(Vector2 pos, float Radius)
        {
            Position = pos;
            CollisionRadius = Radius;
            range = (int)(CollisionRadius+50);
            weight = 100000;
        }
        public override void KillEntity(GameData data)
        {
            Position = new Vector2(MathHelper.CutFloat(Position.X, 1, data.MapSize.X - 1), MathHelper.CutFloat(Position.Y, 1, data.MapSize.Y - 1));
            IsDead = true;
            for (int i = 0; i < GetRandomValue(150, 200); i++)
            {
                Vector3 tmpColor = ColorToHSV(RED);
                tmpColor.X += GetRandomValue(-30, 15);
                data.particles.Add(new Explosion(Position, i * GetRandomValue(0, 360), ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), GetRandomValue(40, 80)));
            }
            data.particles.Add(new Score(ScoreDrop.ToString(),Position, GetRandomValue(0, 360), Raylib_cs.Color.LIME, GetRandomValue(40, 80)));
        }
        public void Update(GameData data)
        {
            foreach (var bullet in data.bullets)
                if (CheckCollisionCircles(bullet.Position, bullet.CollisionRadius, Position, CollisionRadius))
                {
                    CollisionRadius -= 3f;
                    bullet.KillEntity(data);
                }
            if (CollisionRadius <= 30 && !active)
            {
                range = (int)(CollisionRadius * 3);
                active = true;
            }
            if (active)
            {
                weight = (int)(-(200 - CollisionRadius) * 300);
                float pLength = (data.Player.Position - Position).Length() - (data.Player.CollisionRadius + CollisionRadius + 300);
                if (pLength < 0)
                {
                    data.Player.Position += (data.Player.Position - Position) / 8000 * pLength;
                }
                foreach (var item in data.entities)
                {
                    if (item.IsDead || item.Range == 0.0f || (item == this)) continue;
                    float mLength = (item.Position - Position).Length() - (item.CollisionRadius + CollisionRadius + 250);
                    if (mLength < 0)
                    {
                        item.Position += (item.Position - Position) / 550000 * -mLength*mLength;
                        if (mLength < -250)
                        {
                            if (item.GetType() == this.GetType())
                            {

                            }
                            else
                            {
                                ScoreDrop += item.ScoreDrop;
                                item.KillEntity(data);
                                CollisionRadius += 0.5f;
                            }
                        }
                    }
                }
            }
            if (CollisionRadius <= 20)
            {
                KillEntity(data);
                data.Score += ScoreDrop;
            }
            if (CollisionRadius >= 100)
            {
                KillEntity(data);
                data.Score += ScoreDrop;

            }
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            graphics.DrawBlackHole(Position + camera.Pos, CollisionRadius);
        }
    }
}
