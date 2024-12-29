using Microsoft.Xna.Framework;

namespace Frith.Components
{
	public struct TransformComponent
	{
		public Vector2 Position { get; set; }

		public Vector2 LocalPosition { get; set; }

		public Entity? Parent { get; set; }

		public bool HadParentInLastFrame { get; set; }

		public Vector2 Scale { get; set; }

		public float Rotation { get; set; }

		public TransformComponent(Vector2 position = default, Vector2 scale = default, float rotation = 0.0f)
        {
			if (scale == default)
				scale = Vector2.One;

			this.LocalPosition = position;
			this.Scale = scale;
			this.Rotation = rotation;

			HadParentInLastFrame = false;

        }
    }
}
