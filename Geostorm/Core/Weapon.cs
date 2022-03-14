using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities;
using Geostorm.Core.Events;

namespace Geostorm.Core
{
    class Weapon
    {
        int level;
        float frequency;
        float timer;
        float speed;

        public Weapon()
        {
            level = 0;
        }


        public void Update(in GameInputs inputs, GameData data, List<Event> events) 
        {
            if (inputs.Shoot)
            {
                Bullet b = new Bullet();
                b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                b.Rotation = data.Player.WeaponRotation;
                b.Velocity =MathHelper.GetVectorRot(b.Rotation)*20;
                b.IsDead = false;

                //data.AddBulletDelayed(b);
                data.AddBulletDelayed(b);

                // Optional
                BulletShootEvent shootEvent = new BulletShootEvent();
                shootEvent.Bullet = b;
                events.Add(shootEvent);
            }
        }
    }
}
