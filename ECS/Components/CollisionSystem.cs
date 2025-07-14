using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public class CollisionSystem : EcsSystem
	{

		public CollisionSystem()
		{
			RequireComponent<TransformComponent>();
			RequireComponent<BoxColliderComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);


			var entities = GetSystemEntities();

			for (int i = 0; i < entities.Count; i++)
			{
				Entity a = entities[i];
				var aTransform = a.GetComponent<TransformComponent>();
				var aCollider = a.GetComponent<BoxColliderComponent>();

				for (int j = i; i < entities.Count; j++)
				{
					Entity b = entities[j];

					if (a == b)
						continue;

					var bTransfrom = b.GetComponent<TransformComponent>();
					var bCollider = b.GetComponent<BoxColliderComponent>();

					bool collisionHappened = aCollider.Bounds.Intersects(bCollider.Bounds);

					if (collisionHappened)
						Logger.Info($"Entity {a.GetId()} is colliding with {b.GetId()}");
					
				}
			}
		}
	}
}
