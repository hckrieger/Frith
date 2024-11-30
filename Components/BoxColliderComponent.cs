using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct BoxColliderComponent
	{
		private Rectangle boundingBox;
        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }

            set
            {
                boundingBox = value;
            }
        }

        public BoxColliderComponent(Rectangle boundingBox = default)
        {
			this.boundingBox = boundingBox;
        }
    }
}
