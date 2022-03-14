using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities
{
    class Bullet : Entity
    {   
        public Bullet()
        {
            CollisionRadius = 15;
        }
        public override void Update(in GameInputs inputs, GameData data, List<Events.Event> events) 
        {
            Position += Velocity;

            if(Position != new Vector2(MathHelper.CutFloat(Position.X, CollisionRadius, data.MapSize.X - CollisionRadius), MathHelper.CutFloat(Position.Y, CollisionRadius, data.MapSize.Y - CollisionRadius)))
            {
                IsDead = true;
            }
            
        }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawBullet(Position + camera.Pos, Rotation);
        }
    }
}
