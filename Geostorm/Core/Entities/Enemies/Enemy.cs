using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using Geostorm.Core.Events;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities.Enemies
{
    class Enemy : Entity
    {
        protected int spawnTime;

        public override int Range { get => spawnTime > 0 ? 0 : range; }

        public int SpawnTime { get => spawnTime;}

        public sealed override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            spawnTime--;
            if (spawnTime <= 0)
            {
                DoUpdate(inputs, data, events);
            }
        }

        public virtual void DoUpdate(in GameInputs inputs, GameData data, List<Event> events) { }
        public sealed override void Draw(Graphics graphics, Camera camera)
        {
            if (spawnTime <= 0) DoDraw(graphics,camera);
        }

        public virtual void DoDraw(Graphics graphics, Camera camera) { }
    }
}
