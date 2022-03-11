using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;

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

            Position.X = 200;
            Position.Y = 200;
        }


        public void Update() 
        {
            Rotation++;
        }
        public override void Draw(Graphics graphics) 
        {
            graphics.DrawPlayer(Position, Rotation);
        }
    }
}
