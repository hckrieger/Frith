using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Events
{
	public class CollisionEvent : Event
	{
        public Entity EntityA { get; set; }
        public Entity EntityB { get; set; }

        public CollisionEvent(Entity a, Entity b) 
        {
            EntityA = a;
            EntityB = b;
        }
    }
}
