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
        int life = 5;
        public int Life { get { return life; } set { life = value; } }
        int cooldown = 0;
        Weapon weapon;
        float targetRotation = 0;
        public float WeaponRotation = 0;

        int WeaponLevel;
        public Player()
        {
            weapon = new Weapon0();
            CollisionRadius = 20;
            Position.X = 200;
            Position.Y = 200;
            weight = 2222222;
            range = 100;
        }

        public void RemoveLife(GameData data)
        {
            foreach (var entity in data.entities)
                entity.KillEntity(data);
            life -= 1;
            if (life <= 0)
                IsDead = true;
            cooldown = 120;
        }

        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            foreach (var enemies in data.enemies)
                if (CheckCollisionCircles(enemies.Position, enemies.CollisionRadius, Position, CollisionRadius))
                    RemoveLife(data);

            if (inputs.MoveAxis.LengthSquared() != 0.0f)
            {
                Velocity += inputs.MoveAxis * 3;

                for (int i = 0; i < data.rng.Next(4, 10); i++)
                {
                    Vector2 p1 = new Vector2(-15, data.rng.Next(-2,2));
                    Matrix3x2 rotate = Matrix3x2.CreateRotation(MathHelper.ToRadians(Rotation));
                    Vector2 curentP = Vector2.Transform(p1, rotate) + Position;
                    Vector3 tmpColor = ColorToHSV(Raylib_cs.Color.ORANGE);
                    tmpColor.X += data.rng.Next(-15, 15);
                    data.particles.Add(new Geostorm.Renderer.Particles.Fire(curentP, data.rng.Next(-15,15)+(Rotation - 180), ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z)));
                }
            }
            if (MathHelper.GetRotation(Velocity, ref targetRotation))
            {
                targetRotation = MathHelper.CutFloat(MathHelper.ModuloFloat(Rotation - targetRotation, -180.0f, 180.0f), -20.0f, 20.0f);
                Rotation = (Rotation - targetRotation) % 360.0f;
            }
            Vector2 weaponDir = inputs.ShootTarget - Position;
            MathHelper.GetRotation(weaponDir, ref WeaponRotation);
            Velocity *= 0.7f;
            Position += Velocity;
            Position = new Vector2(MathHelper.CutFloat(Position.X, CollisionRadius, data.MapSize.X - CollisionRadius), MathHelper.CutFloat(Position.Y, CollisionRadius, data.MapSize.Y - CollisionRadius));

            weapon.Update(inputs, data, events);
            if (cooldown > 0)
                cooldown--;

            switch (WeaponLevel)
            {
                case 0:
                    if (data.Score > 1250)
                    {
                        weapon = new Weapon1();
                        WeaponLevel++;
                    }
                    break;
                case 1:
                    if (data.Score > 2500)
                    {
                        weapon = new Weapon2();
                        WeaponLevel++;
                    }
                    break;
                case 2:
                    if (data.Score > 3750)
                    {
                        weapon = new Weapon3();
                        WeaponLevel++;
                    }
                    break;
                case 3:
                    if (data.Score > 5000)
                    {
                        weapon = new Weapon4();
                        WeaponLevel++;
                    }
                    break;
                case 4:
                    if (data.Score > 6250)
                    {
                        weapon = new Weapon5();
                        WeaponLevel++;
                    }
                    break;
                case 5:
                    if (data.Score > 7500)
                    {
                        weapon = new Weapon6();
                        WeaponLevel++;
                    }
                    break;
                case 6:
                    if (data.Score > 8125)
                    {
                        weapon = new Weapon7();
                        WeaponLevel++;
                    }
                    break;
                case 7:
                    if (data.Score > 8750)
                    {
                        weapon = new Weapon8();
                        WeaponLevel++;
                    }
                    break;
                default:
                    break;
            }
            System.Console.WriteLine(data.Score);
        }
        public override void Draw(Graphics graphics, Camera camera)
        {
            if (cooldown % 6 == 0 || cooldown == 0)
                graphics.DrawPlayer(Position + camera.Pos, Rotation, WeaponRotation);
        }
    }
}
