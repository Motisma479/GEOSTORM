using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;

namespace Geostorm.Core
{
    class Game
    {
        List<IGameEventListener> eventListeners;

        void AddEventListener(IGameEventListener listener) { }
        public void Update(GameInputs inputs) { }
        public void Render() { }
    }
}
