using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct BoxColliderComponent
	{
		public Rectangle Bounds { get; set; }

		public Rectangle LocalBounds { get; private set; }

		public BoxColliderComponent(Rectangle localBounds)
		{
			LocalBounds = localBounds;

			//Bounds = new Rectangle((int)offset.X, (int)offset.Y, width, height);
		}
	}
}
