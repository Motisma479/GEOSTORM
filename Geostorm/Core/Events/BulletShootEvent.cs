using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geostorm.Core.Entities.Enemies;
using Geostorm.Core.Entities;

namespace Geostorm.Core.Events
{
    class BulletShootEvent : Event
    {
        Bullet bullet = new Bullet();
    }
}
