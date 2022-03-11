using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;
using Geostorm.Renderer;
using System.Numerics;

namespace Geostorm.Core
{
    class Game
    {
        List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public Game()
        {

        }

        public void AddEventListener(IGameEventListener listener) 
        { 
        }
        public void Update(GameInputs inputs) 
        {
        
        }
        public void Render(Graphics graphics) 
        {
            graphics.DrawPlayer(new Vector2(0,0), 0);
            graphics.DrawGrunt(new Vector2(0, 0), 0);
            graphics.DrawBullet(new Vector2(0, 0), 0);
        }
    }
}
