using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using Geostorm.Core.Events;



namespace Geostorm.Core.Entities.Enemies
{
    class Enemy : Entity
    {
        int spawnTime;
        public sealed override void Update(in GameInputs inputs, GameData datas, List<Event> events) { }
        public override void Draw() { }
    }
}
