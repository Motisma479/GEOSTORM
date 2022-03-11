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
        float targetRotation = 0;
        public float WeaponRotation = 0;
        public Player()
        {
            Weapon = new Weapon();

            Position.X = 200;
            Position.Y = 200;
        }


        public void Update(GameInputs inputs, Vector2 mapSize)
        {
            if (inputs.MoveAxis.LengthSquared() != 0.0f)
            {
                Velocity += inputs.MoveAxis*3;
            }
            if (MathHelper.getRotation(Velocity,ref targetRotation))
            {
                targetRotation = MathHelper.cutFloat(MathHelper.ModuloFloat(Rotation - targetRotation,-180.0f,180.0f), -20.0f,20.0f);
                Rotation = (Rotation - targetRotation) % 360.0f;
            }
            Vector2 weaponDir = inputs.ShootTarget - Position;
            MathHelper.getRotation(weaponDir,ref WeaponRotation);
            Velocity *= 0.7f;
            Position += Velocity;
            Position = new Vector2(MathHelper.cutFloat(Position.X,20,mapSize.X-20), MathHelper.cutFloat(Position.Y, 20, mapSize.Y-20));
        }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawPlayer(Position+camera.Pos, Rotation, WeaponRotation);
        }
    }
}
