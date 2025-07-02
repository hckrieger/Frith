using Frith.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class MovementSystem : EcsSystem
	{

		public MovementSystem()
		{
			RequireComponent<TransformComponent>();
			RequireComponent<RigidBodyComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in GetSystemEntities())
			{
				ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
				var rigidbody = entity.GetComponent<RigidBodyComponent>();

				transform.Position += new Vector2(rigidbody.Velocity.X, rigidbody.Velocity.Y) * (float)gameTime.ElapsedGameTime.TotalSeconds;

				
			}
		}
	}
}
