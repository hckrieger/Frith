using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct TransformComponent
	{
		public Vector2 Position { get; set; }
		public Vector2 Scale { get; set; }
		public float Rotation { get; set; }

		public TransformComponent(Vector2 position = default, Vector2 scale = default, float rotation = 0.0f)
		{
			Position = position;
			if (scale != default)
			{
				Scale = scale;
			}
			{
				Scale = Vector2.One;
			}
			Rotation = rotation;
		}
	}
}
