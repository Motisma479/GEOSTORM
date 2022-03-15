using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using Geostorm.Core.Events;
using Geostorm.Renderer;

namespace Geostorm.Core.Entities.Enemies
{
    class Grunt : Enemy
    {
        public Grunt(int spawnTime)
        {
            this.spawnTime = spawnTime;
            weight = 5000000;
            range = 140;
            Position = new Vector2(100, 100);
        }

        float rotation;
        Vector2 renderScale = new Vector2(1,1);

        public override void DoUpdate(in GameInputs inputs, GameData data, List<Event> events)
        {
            Position += new Vector2(1,1);
            renderScale = new Vector2(0.9f+0.2f*MathF.Sin(inputs.TotalTime), 0.9f + 0.2f * MathF.Cos(inputs.TotalTime));
        }
        public override void DoDraw(Graphics graphics, Camera camera) 
        {
            graphics.DrawGrunt(Position + camera.Pos, renderScale, rotation, spawnTime);
        }
    }
}
