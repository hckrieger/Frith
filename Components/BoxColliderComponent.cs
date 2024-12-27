using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct BoxColliderComponent(Rectangle boundingBox = default)
	{
        public Rectangle BoundingBox { get; set; } = boundingBox;
	}
}
