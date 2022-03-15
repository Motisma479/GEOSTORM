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
        float attraction = 0.01f;
        public BlackHole(Vector2 pos, float Radius)
        {
            Position = pos;
            CollisionRadius = Radius;
        }
        public override void KillEntity(GameData data)
        {
            Position = new Vector2(MathHelper.CutFloat(Position.X, 1, data.MapSize.X - 1), MathHelper.CutFloat(Position.Y, 1, data.MapSize.Y - 1));
            IsDead = true;
            for (int i = 0; i < GetRandomValue(100, 150); i++)
            {
                Vector3 tmpColor = ColorToHSV(RED);
                tmpColor.X += GetRandomValue(-30, 15);
                data.particles.Add(new Explosion(Position, i * GetRandomValue(0, 360), ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), GetRandomValue(40, 80)));
            }
        }
        public void Update(GameData data)
        {
            foreach (var bullet in data.bullets)
                if (CheckCollisionCircles(bullet.Position, bullet.CollisionRadius, Position, CollisionRadius))
                {
                    CollisionRadius -= 1f;
                }
            if (MathHelper.SuperiorOrEqual(data.Player.Position, Position + new Vector2(-25, -25)) && MathHelper.InferiorOrEqual(data.Player.Position, Position + new Vector2(25, 25)))
                data.Player.RemoveLife(data);
            if (CollisionRadius <= 30)
                active = true;
            if (active)
            {
                if (CheckCollisionCircles(data.Player.Position, data.Player.CollisionRadius, Position, CollisionRadius + 50))
                {
                    attraction = 1f;
                    if (CheckCollisionCircles(data.Player.Position, data.Player.CollisionRadius, Position, CollisionRadius - 30))
                        attraction = 3f;
                    if (data.Player.Position.X < Position.X)
                        data.Player.Position.X += attraction;
                    else if (data.Player.Position.X > Position.X)
                        data.Player.Position.X -= attraction;
                    if (data.Player.Position.Y < Position.X)
                        data.Player.Position.Y += attraction;
                    else if (data.Player.Position.Y > Position.X)
                        data.Player.Position.Y -= attraction;
                }
                CollisionRadius += 0.1f;
            }
            if (CollisionRadius <= 20)
            {
                IsDead = true;
            }
            if (CollisionRadius >= 100)
            {
                IsDead = true;

            }
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            graphics.DrawBlackHole(Position + camera.Pos, CollisionRadius);
        }
    }
}
