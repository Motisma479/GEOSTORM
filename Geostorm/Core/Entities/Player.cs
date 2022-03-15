using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;
using Geostorm.Core.Events;
using static Raylib_cs.Raylib;

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
            CollisionRadius = 20;
            Position.X = 200;
            Position.Y = 200;
        }


        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            if (inputs.MoveAxis.LengthSquared() != 0.0f)
            {
                Velocity += inputs.MoveAxis*3;

                for (int i = 0; i < GetRandomValue(4, 10); i++)
                {
                    Vector2 p1 = new Vector2(-15, GetRandomValue(-2,2));
                    Matrix3x2 rotate = Matrix3x2.CreateRotation(MathHelper.ToRadians(Rotation));
                    Vector2 curentP = Vector2.Transform(p1, rotate) + Position;
                    Vector3 tmpColor = ColorToHSV(Raylib_cs.Color.ORANGE);
                    tmpColor.X += GetRandomValue(-15, 15);
                    data.particles.Add(new Geostorm.Renderer.Particles.Fire(curentP, GetRandomValue((int)(Rotation - 180 + 15), (int)(Rotation - 180 - 15)), ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z)));
                }
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
            Position = new Vector2(MathHelper.CutFloat(Position.X,CollisionRadius, data.MapSize.X-CollisionRadius), MathHelper.CutFloat(Position.Y, CollisionRadius, data.MapSize.Y-CollisionRadius));

            weapon.Update(inputs, data, events);
        }
        public override void Draw(Graphics graphics, Camera camera) 
        {
            graphics.DrawPlayer(Position+camera.Pos, Rotation, WeaponRotation);
        }
    }
}
