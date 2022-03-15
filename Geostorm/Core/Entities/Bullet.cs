using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;
using Geostorm.Core.Events;
using Geostorm.Renderer.Particles;
using Raylib_cs;

namespace Geostorm.Core.Entities
{
    class Bullet : Entity
    {
        public Bullet()
        {
            CollisionRadius = 5;
            weight = 222222;
            range = 70;
        }
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            Position += Velocity;
            if (!Raylib.CheckCollisionPointRec(Position, new Rectangle(CollisionRadius * 2, CollisionRadius * 2, data.MapSize.X - CollisionRadius * 4, data.MapSize.Y - CollisionRadius * 4)))
            {
                KillEntity(data);
            }
        }

        public override void KillEntity(GameData data)
        {
            Position = new Vector2(MathHelper.CutFloat(Position.X, 1, data.MapSize.X-1), MathHelper.CutFloat(Position.Y, 1, data.MapSize.Y-1));
            IsDead = true;
            for (int i = 0; i < data.rng.Next(20, 30); i++)
            {
                Vector3 tmpColor = Raylib.ColorToHSV(Color.YELLOW);
                tmpColor.X += data.rng.Next(-30, 15);
                data.particles.Add(new Explosion(Position, data.rng.Next(0, 360), Raylib.ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), data.rng.Next(40, 80)));
            }
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            graphics.DrawBullet(Position + camera.Pos, Rotation);
        }
    }
}
