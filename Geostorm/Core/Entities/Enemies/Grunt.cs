﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core;
using System.Numerics;
using Geostorm.Core.Events;
using Geostorm.Renderer;
using static Raylib_cs.Raylib;
using Raylib_cs;
using Geostorm.Renderer.Particles;

namespace Geostorm.Core.Entities.Enemies
{
    class Grunt : Enemy
    {
        public Grunt(int spawnTime, GameData datas, int lvl = 1)
        {
            this.spawnTime = spawnTime;
            weight = 5000000;
            range = 140;
            CollisionRadius = 18;
            Position = new Vector2(datas.rng.Next(20, (int)(datas.MapSize.X- CollisionRadius - 5)), datas.rng.Next(20, (int)(datas.MapSize.Y - CollisionRadius - 5)));
            Level = lvl;
        }
        float targetRotation = 0;
        Vector2 renderScale = new Vector2(1,1);

        public override void DoUpdate(in GameInputs inputs, GameData data, List<Event> events)
        {
            if (MathHelper.GetRotation(data.Player.Position- Position, ref targetRotation))
            {
                targetRotation = MathHelper.CutFloat(MathHelper.ModuloFloat(Rotation - targetRotation, -180.0f, 180.0f), -10.0f, 10.0f);
                Rotation = (Rotation - targetRotation) % 360.0f;
            }
            Position += MathHelper.GetVectorRot(Rotation)*2;
            float deform = 0.2f*MathF.Sin(data.TotalTime * 6);
            renderScale = new Vector2(0.9f+deform,0.9f-deform);
        }

        public override void KillEntity(GameData data)
        {
            Position = new Vector2(MathHelper.CutFloat(Position.X, 0, data.MapSize.X), MathHelper.CutFloat(Position.Y, 0, data.MapSize.Y));
            IsDead = true;
            for (int i = 0; i < data.rng.Next(10, 20); i++)
            {
                Vector3 tmpColor = Raylib.ColorToHSV(Color.BLUE);
                tmpColor.Y += data.rng.Next(-30, 15);
                data.particles.Add(new Explosion(Position, i * data.rng.Next(0,360), Raylib.ColorFromHSV(tmpColor.X, tmpColor.Y, tmpColor.Z), data.rng.Next(40, 80)));
            }
        }
        public override void DoDraw(Graphics graphics, Camera camera) 
        {
            graphics.DrawGrunt(Position + camera.Pos, renderScale, 0, spawnTime);
        }
    }
}
