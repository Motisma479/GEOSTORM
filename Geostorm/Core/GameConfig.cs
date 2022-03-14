using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Geostorm.Core
{
    class GameConfig
    {
        public bool GamepadRelativeAim = false;
        public int SelectedGamepad = -1;
        public InputKey[] KeyboardInputs = { new InputKey(0,87), new InputKey(0, 65), new InputKey(0, 83), new InputKey(0, 68), new InputKey(0, 32) }; // WASD SPACE
        public string[] InputStrings = { "MovementUp", "MovementLeft", "MovementDown", "MovementRight", "ActionShoot" };

        public void LoadConfigFile()
        {
            var installDirectory = AppContext.BaseDirectory;
            string[] inputs = File.ReadAllLines(installDirectory + "inputs.txt");
            for (int l = 0; l < inputs.Length; l++)
            {
                if (inputs[l].Length == 0 || inputs[l][0] == 0 || inputs[l][0] == '#') continue;
                for (int i = 0; i < InputStrings.Length; i++)
                {
                    if (inputs[l].Contains(InputStrings[i]))
                    {
                        string line = inputs[l].Remove(0,InputStrings[i].Length+1);
                        if (int.TryParse(line, out int j))
                        {
                            int indf = line.IndexOf(' ');
                            if (indf < 0) continue;
                            line = line.Substring(0,indf+1);
                            if (int.TryParse(line, out int k))
                            {
                                KeyboardInputs[i] = new InputKey(j,k);
                            }
                        }
                        continue;
                    }
                }
            }
        }
        public void WriteConfigFile()
        {
            var installDirectory = AppContext.BaseDirectory;
            string[] inputs = File.ReadAllLines(installDirectory + "inputs.txt");
            for (int l = 0; l < inputs.Length; l++)
            {
                if (inputs[l].Length == 0 || inputs[l][0] == 0 || inputs[l][0] == '#') continue;
                for (int i = 0; i < InputStrings.Length; i++)
                {
                    if (inputs[l].Contains(InputStrings[i]))
                    {
                        string line = InputStrings[i] + ' ' + KeyboardInputs[i].ToString();
                        inputs[l] = line;
                        continue;
                    }
                }
            }
            File.WriteAllLines(installDirectory + "inputs.txt",inputs);
        }
    }
}
