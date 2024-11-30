using Microsoft.Xna.Framework;

namespace Frith.Components
{
	public struct RigidBodyComponent
	{
		private Vector2 velocity;
        public Vector2 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public RigidBodyComponent(Vector2 velocity = default)
        {
            this.velocity = velocity;
        }
    }
}
