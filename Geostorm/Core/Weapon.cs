using System;
using System.Collections.Generic;
using System.Linq;
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
                b.Velocity = MathHelper.GetVectorRot(b.Rotation);
                b.IsDead = false;
                
                //data.AddBulletDelayed(b);
                data.bullets.Add(b);

                // Optional
                BulletShootEvent shootEvent = new BulletShootEvent();
                shootEvent.Bullet = b;
                events.Add(shootEvent);
            }
            for (int i = 0; i < data.bullets.Count; i++)
                if (data.bullets[i].IsDead)
                    data.bullets.RemoveAt(i);
            
        }
    }
}
