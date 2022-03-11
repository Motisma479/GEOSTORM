using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Events;
using Geostorm.Renderer;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace Geostorm.Core
{
    class Game
    {
        GameData datas;
        List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public Game()
        {
            datas = new GameData();
        }

        public void AddEventListener(IGameEventListener listener)
        {

        }
        public void Update(GameInputs inputs)
        {
            datas.camera.Update(inputs);
            for (int i = 0; i < datas.stars.Count(); i++)
                datas.stars[i].Update(datas.camera);
        }
        public void Render(Graphics graphics)
        {
            for (int i = 0; i < datas.stars.Count(); i++)
                if (IsInside(datas.camera.Pos + datas.stars[i].Pos))
                    datas.stars[i].Draw(graphics);
            graphics.DrawMap(datas.MapSize, datas.camera.Pos);

        }
        public bool IsInside(Vector2 pos)
        {
            if ((pos.X < GetScreenWidth() && pos.X > 0) && (pos.Y < GetScreenHeight() && pos.Y > 0))
                return true;
            else
                return false;
        }
    }
}
