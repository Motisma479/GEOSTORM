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

        /*protected float frequency;
        protected float timer;
        protected float speed = 20;*/
        protected float coolDown;

        public Weapon()
        {
        }

        protected void addBullet(GameData data, float distance)
        {
            Bullet b = new Bullet();
            b.Position = data.Player.Position + MathHelper.GetVectorRot(data.Player.WeaponRotation) * 23;
            b.Rotation = data.Player.WeaponRotation;
            b.Position += MathHelper.GetVectorRot(data.Player.WeaponRotation + 90) * distance;
            b.Velocity = MathHelper.GetVectorRot(b.Rotation) * 20;
            b.IsDead = false;
            data.AddBulletDelayed(b);
            //coolDown = 1/6.0f;
        }
        protected void addBulletStatic(GameData data, float degree)
        {
            Bullet b = new Bullet();
            b.Position = data.Player.Position + MathHelper.GetVectorRot(degree) * 23;
            b.Rotation = degree;
            b.Velocity = MathHelper.GetVectorRot(b.Rotation)* 20;
            data.AddBulletDelayed(b);
        }

    public virtual void Update(in GameInputs inputs, GameData data, List<Event> events) 
        {
            if (inputs.Shoot && coolDown <= 0)
            {
                // Optional
                /*BulletShootEvent shootEvent = new BulletShootEvent();
                shootEvent.Bullet = b;
                events.Add(shootEvent);*/
            }
        }
    }
    class Weapon0 : Weapon
    {
        public Weapon0()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data,0);
                coolDown = 1 / 6.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon1 : Weapon
    {
        public Weapon1()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data,0);
                coolDown = 1 / 12.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon2 : Weapon
    {
        public Weapon2()
        { }
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {

                //addBullet(data, MathF.Sin(data.TotalTime));
                coolDown = 1 / 6.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon3 : Weapon
    {
        public Weapon3()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data,-10);
                addBullet(data, 10);
                coolDown = 1 / 6.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon4 : Weapon
    {
        public Weapon4()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data, -10);
                addBullet(data, 10);
                coolDown = 1 / 12.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon5 : Weapon
    {
        public Weapon5()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data, 0);
                coolDown = 1 / 60.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon6 : Weapon
    {
        public Weapon6()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data, -10);
                addBullet(data, 10);
                coolDown = 1 / 60.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon7 : Weapon
    {
        public Weapon7()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {

            if (inputs.Shoot && coolDown <= 0)
            {
                addBullet(data,-10);
                addBullet(data,  0);
                addBullet(data, 10);
                coolDown = 1 / 60.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
    class Weapon8 : Weapon
    {
        public Weapon8()
        {}
        public override void Update(in GameInputs inputs, GameData data, List<Event> events)
        {
            if (inputs.Shoot && coolDown <= 0)
            {
                for (int i = 0; i < 360; i++)
                {
                    addBulletStatic(data, i);
                }
                coolDown = 2.0f;
            }
            else
                coolDown -= data.DeltaTime;
        }
    }
}
