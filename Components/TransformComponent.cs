using Microsoft.Xna.Framework;

namespace Frith.Components
{
	public struct TransformComponent
	{
		private Vector2 position;
		public Vector2 Position
		{
			get => position;
			set => position = value;
		}

		private Vector2 scale;
		public Vector2 Scale
		{
			get => scale;
			set => scale = value;
		}

		private float rotation;
		public float Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

        public TransformComponent(Vector2 position = default, Vector2 scale = default, float rotation = 0.0f)
        {
			if (scale == default)
				scale = Vector2.One;

			this.position = position;
			this.scale = scale;
			this.rotation = rotation;


        }
    }
}
