﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;
using Geostorm.Core.Events;

namespace Geostorm.Core.Entities
{
    class Player : Entity
    {
        int Life { get { return Life; } set { Life = value; } }
        int Score { get { return Score; } set { Score = value; } }
        Weapon weapon;
        float targetRotation = 0;
        public float WeaponRotation = 0;
        public Player()
        {
            weapon = new Weapon();

            Position.X = 200;
            Position.Y = 200;
        }


        public void Update(GameInputs inputs, GameData data, List<Event> events)
        {
            if (inputs.MoveAxis.LengthSquared() != 0.0f)
            {
                Velocity += inputs.MoveAxis*3;
            }
            if (MathHelper.GetRotation(Velocity,ref targetRotation))
            {
                targetRotation = MathHelper.CutFloat(MathHelper.ModuloFloat(Rotation - targetRotation,-180.0f,180.0f), -20.0f,20.0f);
                Rotation = (Rotation - targetRotation) % 360.0f;
            }
            Vector2 weaponDir = inputs.ShootTarget - Position;
            MathHelper.GetRotation(weaponDir,ref WeaponRotation);
            Velocity *= 0.7f;
            Position += Velocity;
            Position = new Vector2(MathHelper.CutFloat(Position.X,20, data.MapSize.X-20), MathHelper.CutFloat(Position.Y, 20, data.MapSize.Y-20));

            weapon.Update(inputs, data, events);
        }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawPlayer(Position+camera.Pos, Rotation, WeaponRotation);
        }
    }
}
