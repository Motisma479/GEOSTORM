using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Core.Events;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities
{
    class Entity
    {
        public bool IsDead;
        public Vector2 Position;
        public float Rotation;
        public float CollisionRadius;

        public virtual void Update(in GameInputs inputs, GameData datas, List<Event> events) { }
        public virtual void Draw(Graphics graphics) { }
    }
}
