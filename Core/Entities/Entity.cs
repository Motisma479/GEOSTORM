using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
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
        public int Weight { get => weight;}
        public virtual int Range { get => range; }

        public int ScoreDrop;

        public virtual void KillEntity(GameData data) { }
        public virtual void Update(in GameInputs inputs, GameData data) { }
        public virtual void Draw(Camera camera) { }
    }
}
