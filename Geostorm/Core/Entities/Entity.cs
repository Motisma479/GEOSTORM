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
        public Vector2 Velocity = new Vector2();
        public float Rotation;
        public float CollisionRadius;
        protected int weight;
        protected int range;
        public readonly int Level;
        public int Weight { get => weight;}
        public virtual int Range { get => range; }

        public virtual void KillEntity(GameData data) { }
        public virtual void Update(in GameInputs inputs, GameData data, List<Event> events) { }
        public virtual void Draw(Graphics graphics, Camera camera) { }
    }
}
