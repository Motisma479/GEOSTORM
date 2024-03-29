﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using Raylib_cs;
using Geostorm.Renderer.Particles;

namespace Geostorm.Core.Entities.Enemies
{
    class Mill : Enemy
    {
        public Mill(int spawnTime, GameData datas, int lvl = 1)
        {
            this.spawnTime = spawnTime;
            weight = 5000000;
            range = 140;
            CollisionRadius = 20;
            Position = new Vector2(datas.rng.Next(20, (int)(datas.MapSize.X- CollisionRadius - 5)), datas.rng.Next(20, (int)(datas.MapSize.Y - CollisionRadius - 5)));
            Level = lvl;

            ScoreDrop = 200;
        }

        public override void DoUpdate(in GameInputs inputs, GameData data)
        {
            Rotation += data.DeltaTime * 240;
            Vector2 dir = (data.player.Position - Position);
            if (dir.LengthSquared() != 0)
            {
                Velocity = Velocity * 0.8f + (dir / dir.Length()) * 0.8f;
            }
            foreach (var item in data.bullets)
            {
                if (item.IsDead) continue;
                Vector2 dir2 = (Position - item.Position);
                if (dir2.Length() < 150)
                {
                    Velocity = Velocity * 0.8f + dir2 * 0.04f;
                }
            }
            Position += Velocity;
            Position = new Vector2(MathHelper.CutFloat(Position.X,CollisionRadius, data.MapSize.X- CollisionRadius), MathHelper.CutFloat(Position.Y, CollisionRadius, data.MapSize.Y - CollisionRadius));
            bool hit = false;
            foreach (var item in data.bullets)
            {
                if (item.IsDead) continue;
                if ((item.Position - Position).Length() < (item.CollisionRadius + CollisionRadius))
                {
                    hit = true;
                    item.KillEntity(data);
                    break;
                }
            }
            if (hit)
            {
                KillEntity(data);
                data.Score+=ScoreDrop;
            }
        }

        public override void KillEntity(GameData data)
        {
            Position = new Vector2(MathHelper.CutFloat(Position.X, 0, data.MapSize.X), MathHelper.CutFloat(Position.Y, 0, data.MapSize.Y));
            IsDead = true;
            if (range == 0.0f) return;
            for (int i = 0; i < data.rng.Next(30, 40); i++)
            {
                Vector3 tmpColor = Raylib.ColorToHSV(Color.PURPLE);
                tmpColor.X += data.rng.Next(-30, 15);
                data.particles.Add(new Explosion(Position, i * data.rng.Next(0,360), Raylib.ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), data.rng.Next(40, 80)));
            }
            data.particles.Add(new Score(ScoreDrop.ToString(), Position, GetRandomValue(0, 360), Raylib_cs.Color.LIME, GetRandomValue(40, 80)));
        }
        public override void DoDraw(Camera camera) 
        {
            Graphics.DrawMill(Position + camera.Pos, Rotation, spawnTime);
        }
    }
}
