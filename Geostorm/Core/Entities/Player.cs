using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Geostorm.Core.Entities
{
    class Player : Entity
    {
        int Life { get { return Life; } set { Life = value; } }
        int Score { get { return Score; } set { Score = value; } }
        Weapon Weapon;

        public Player()
        {
            Weapon = new Weapon();
        }


        public void Update() { }
        public void Draw() 
        {
        }
    }
}
