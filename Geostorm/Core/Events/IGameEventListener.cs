using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geostorm.Core.Events
{
    abstract class IGameEventListener
    {
        public abstract void HandleEvents(IEnumerable<Event> events, GameData data);
    }
}
