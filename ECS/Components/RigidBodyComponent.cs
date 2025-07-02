using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct RigidBodyComponent
	{
		public Vector2 Velocity { get; set; }
		public RigidBodyComponent(Vector2 velocity = default)
		{
			Velocity = velocity;
		}
	}
}
