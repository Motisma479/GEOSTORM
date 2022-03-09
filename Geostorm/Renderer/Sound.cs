using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;
using Geostorm.Core;

namespace Geostorm.Renderer
{
    class Sound : IGameEventListener
    {
        public void Load() { }
        public void Unload() { }

        public void HandleEvents(IEnumerable<Event> events, GameData data) { }
    }
}
