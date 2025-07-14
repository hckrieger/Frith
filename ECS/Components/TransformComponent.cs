using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct TransformComponent
	{
		public Vector2 Position { get; set; }

		public Vector2 LocalPosition { get; set; }

		public Vector2 Scale { get; set; }
		public Vector2 LocalScale { get; set; }

		public Vector2 Origin { get; set; }
		
		public float Rotation { get; set; }
		public float LocalRotation { get; set; }

		public Entity? Parent { get; set; } = null;

		public bool HadParentInLastFrame { get; set; } = false;

		public TransformComponent(Vector2 position, Vector2 scale = default, float rotation = default, Vector2 origin = default)
		{
			LocalPosition = (position != default) ? position : Vector2.Zero;

			LocalScale = (scale != default) ? scale : Vector2.One;
		 
			LocalRotation = (rotation != default) ? rotation : 0.0f;	

			Origin = (origin != default) ? origin : Vector2.Zero;
		}
	}
}
