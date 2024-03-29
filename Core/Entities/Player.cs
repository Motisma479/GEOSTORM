﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using Geostorm.Renderer.Particles;
using Raylib_cs;

namespace Geostorm.Core.Entities
{
    class Player : Entity
    {
        int life = 5;
        public int Life { get { return life; } set { life = value; } }
        int cooldown = 0;
        Weapon weapon;
        bool godmode;
        float targetRotation = 0;
        public float WeaponRotation = 0;

        public int WeaponLevel;

        public Player()
        {
            weapon = new Weapon0();
            CollisionRadius = 20;
            Position.X = 400;
            Position.Y = 400;
            weight = 2222222;
            range = 100;
        }

        public void RemoveLife(GameData data)
        {
            if (!godmode) {
            foreach (var item in data.enemies)
            {
                 if (!item.IsDead && item.SpawnTime <= 90) item.KillEntity(data);
            }
            foreach (var item in data.blackHoles)
            {
                if (item.IsDead) continue;
                item.KillEntity(data);
            }

                life -= 1;
                weight = 6000000;
                range = 500;
                if (life <= 0)
                    IsDead = true;
                cooldown = 120;
                for (int i = 0; i < data.rng.Next(200, 250); i++)
                {
                    Vector3 tmpColor = Raylib.ColorToHSV(Color.GREEN);
                    tmpColor.X += data.rng.Next(-30, 15);
                    data.particles.Add(new Explosion(Position, data.rng.Next(0, 360), Raylib.ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), data.rng.Next(40, 80)));
                }
            }
        }

        public override void Update(in GameInputs inputs, GameData data)
        {
            if (IsKeyPressed(KeyboardKey.KEY_KP_0))
                godmode = !godmode;
            foreach (var enemy in data.enemies)
            {
                if (enemy.IsDead || enemy.SpawnTime >= 0) continue;
                if ((enemy.Position - Position).Length() < (enemy.CollisionRadius+CollisionRadius))
                    RemoveLife(data);
            }
            foreach (var item in data.blackHoles)
            {
                if (item.IsDead || item.SpawnTime > 0) continue;
                if ((item.Position - Position).Length() < (item.CollisionRadius + CollisionRadius))
                    RemoveLife(data);
            }

            if (inputs.MoveAxis.LengthSquared() != 0.0f)
            {
                Velocity += inputs.MoveAxis * 3;

                for (int i = 0; i < data.rng.Next(4, 10); i++)
                {
                    Vector2 p1 = new Vector2(-15, data.rng.Next(-2, 2));
                    Matrix3x2 rotate = Matrix3x2.CreateRotation(MathHelper.ToRadians(Rotation));
                    Vector2 curentP = Vector2.Transform(p1, rotate) + Position;
                    Vector3 tmpColor = ColorToHSV(Raylib_cs.Color.ORANGE);
                    tmpColor.X += data.rng.Next(-15, 15);
                    data.particles.Add(new Geostorm.Renderer.Particles.Fire(curentP, data.rng.Next(-15, 15) + (Rotation - 180), ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z)));
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

            weapon.Update(inputs, data);
            if (cooldown > 0)
            {
                cooldown--;
                if (cooldown < 70)
                {
                    weight = 2222222;
                    range = 100;
                }
            }
            //
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
                    if (data.Score > 1875)
                    {
                        weapon = new Weapon2();
                        WeaponLevel++;
                    }
                    break;
                case 2:
                    if (data.Score > 2812)
                    {
                        weapon = new Weapon3();
                        WeaponLevel++;
                    }
                    break;
                case 3:
                    if (data.Score > 4218)
                    {
                        weapon = new Weapon4();
                        WeaponLevel++;
                    }
                    break;
                case 4:
                    if (data.Score > 6328)
                    {
                        weapon = new Weapon5();
                        WeaponLevel++;
                    }
                    break;
                case 5:
                    if (data.Score > 9492)
                    {
                        weapon = new Weapon6();
                        WeaponLevel++;
                    }
                    break;
                case 6:
                    if (data.Score > 14238)
                    {
                        weapon = new Weapon7();
                        WeaponLevel++;
                    }
                    break;
                case 7:
                    if (data.Score > 28476)
                    {
                        weapon = new Weapon8();
                        WeaponLevel++;
                    }
                    break;
                default:
                    break;
            }
        }
        public override void Draw(Camera camera)
        {
            if (godmode)
                DrawText("GODMODE", GetScreenWidth() - 100 - MeasureText("GODMODE", 50), GetScreenHeight() - 100, 50, Color.RED);
            Graphics.DrawPlayer(Position + camera.Pos, Rotation, WeaponRotation);
            if (cooldown % 12 == 1)
                DrawCircleLines((int)(Position.X + camera.Pos.X), (int)(Position.Y + camera.Pos.Y), CollisionRadius, Color.WHITE);
        }

        public void ResetWeapon()
        {
            weapon = new Weapon0();
        }
    }
}
