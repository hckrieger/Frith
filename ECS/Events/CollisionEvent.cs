using Frith.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Events
{
	public class CollisionEvent(Entity a, Entity b) : IEvent
	{
		public Entity a = a, b = b;
	}
}
