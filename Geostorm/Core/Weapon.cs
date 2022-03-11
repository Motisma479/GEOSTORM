using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;

namespace Geostorm.Core
{
    class Weapon
    {
        int level;
        float frequency;
        float timer;
        float speed;

        public Weapon()
        {
            level = 0;
        }


        public void Update(in GameInputs inputs, GameData data, List<Event> events) 
        {

        }
    }
}
