using Microsoft.Xna.Framework;

namespace Frith.Components
{
	public struct RigidBodyComponent(Vector2 velocity = default)
    {
        public Vector2 Velocity { get; set; } = velocity;
    }
}
