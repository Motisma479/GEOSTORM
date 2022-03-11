using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geostorm.Core
{
    class GameConfig
    {
        public bool GamepadRelativeAim = false;
        public int SelectedGamepad = -1;
        public int[] KeyboardInputs = { 87, 65, 83, 68, 32}; // WASD SPACE

        public void LoadConfigFile()
        {
            // TODO load options
        }
    }
}
