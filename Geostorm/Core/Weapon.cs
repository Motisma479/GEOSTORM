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
        int coolDown = 0;

        public Weapon()
        {
            level = 8;
        }


        public void Update(in GameInputs inputs, GameData data, List<Event> events) 
        {
            if (inputs.Shoot && coolDown <= 0)
            {
                Bullet b = new Bullet();






                switch (level)
                {
                    case 0:
                        b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                        b.Rotation = data.Player.WeaponRotation;
                        b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                        coolDown = 10;
                        break;
                    case 1:
                        b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                        b.Rotation = data.Player.WeaponRotation;
                        b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                        coolDown = 5;
                        break;
                    case 3:
                        { 
                            b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            b.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * -10;
                            b.Rotation = data.Player.WeaponRotation;
                            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;

                            Bullet c = new Bullet();
                            c.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            c.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * 10;
                            c.Rotation = data.Player.WeaponRotation;
                            c.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                            data.AddBulletDelayed(c);
                            coolDown = 10;
                            break;
                        }
                    case 4:
                        {
                            b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            b.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * -10;
                            b.Rotation = data.Player.WeaponRotation;
                            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;

                            Bullet c = new Bullet();
                            c.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            c.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * 10;
                            c.Rotation = data.Player.WeaponRotation;
                            c.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                            data.AddBulletDelayed(c);
                            coolDown = 5;
                            break;
                        }


                    case 5:
                        b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                        b.Rotation = data.Player.WeaponRotation;
                        b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                        coolDown = 1;
                        break;
                    case 6:
                        {
                            b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            b.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * -10;
                            b.Rotation = data.Player.WeaponRotation;
                            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;

                            Bullet c = new Bullet();
                            c.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            c.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * 10;
                            c.Rotation = data.Player.WeaponRotation;
                            c.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                            data.AddBulletDelayed(c);
                            coolDown = 1;
                            break;
                        }
                    case 7:
                        {
                            b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            b.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * -20;
                            b.Rotation = data.Player.WeaponRotation;
                            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;

                            Bullet c = new Bullet();
                            c.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            c.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * 0;
                            c.Rotation = data.Player.WeaponRotation;
                            c.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                            data.AddBulletDelayed(c);

                            Bullet d = new Bullet();
                            d.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                            d.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * 20;
                            d.Rotation = data.Player.WeaponRotation;
                            d.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                            data.AddBulletDelayed(d);

                            coolDown = 1;
                            break;
                        }
                    case 8:
                        {
                            b.Position = data.Player.Position + MathHelper.GetVectorRot(0) * 23;
                            
                            b.Rotation = 0;
                            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;

                            for (int i=0; i < 360; i++)
                            {
                                Bullet c = new Bullet();
                                c.Position = data.Player.Position + MathHelper.GetVectorRot(i) * 23;
                                c.Rotation = i;
                                c.Velocity = MathHelper.GetVectorRot(c.Rotation) * 20;
                                data.AddBulletDelayed(c);
                            }
                           

                            coolDown = 120;
                            break;
                        }

                    default:
                        b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
                        b.Rotation = data.Player.WeaponRotation;
                        b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
                        coolDown = 10;
                        break;
                }

                b.IsDead = false;

                //data.AddBulletDelayed(b);
                data.AddBulletDelayed(b);

                // Optional
                BulletShootEvent shootEvent = new BulletShootEvent();
                shootEvent.Bullet = b;
                events.Add(shootEvent);
            }
            else
                coolDown--;
            
            
        }
    }
}
