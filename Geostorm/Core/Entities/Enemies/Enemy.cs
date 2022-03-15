using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using Geostorm.Core.Events;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using Raylib_cs;

namespace Geostorm.Core.Entities.Enemies
{
    class Enemy : Entity
    {
        protected float spawnTime;
        protected int Level;

        public override int Range { get => spawnTime > 0 ? 0 : range; }

        public float SpawnTime { get => spawnTime;}

        

        public sealed override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            spawnTime-= 60*data.DeltaTime;
            if (spawnTime >= 60) return;
            if (spawnTime <= 0)
            {
                DoUpdate(inputs, data, events);
            }
            bool hit = false;
            foreach (var item in data.bullets)
            {
                if (item.IsDead) continue;
                if ((item.Position - Position).Length() < (item.CollisionRadius + CollisionRadius))
                {
                    hit = true;
                    item.KillEntity(data);
                    KillEntity(data);
                    break;
                }
            }
        }

        public virtual void DoUpdate(in GameInputs inputs, GameData data, List<Event> events) { }
        public sealed override void Draw(Graphics graphics, Camera camera)
        {
            DoDraw(graphics,camera);
        }

        public virtual void DoDraw(Graphics graphics, Camera camera) { }
    }
}
