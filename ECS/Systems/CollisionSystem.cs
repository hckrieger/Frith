using Frith.ECS.Components;
using Frith.ECS.Events;
using Frith.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class CollisionSystem : EcsSystem
	{
		private EventBus eventBus;
		public CollisionSystem(Game game)
		{
			RequireComponent<TransformComponent>();
			RequireComponent<BoxColliderComponent>();	

			eventBus = game.Services.GetService<EventBus>();

		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var entities = GetSystemEntities();

			for (var i = 0; i < entities.Count; i++)
			{
				Entity a = entities[i];
				var aTransform = a.GetComponent<TransformComponent>();
				var aCollider = a.GetComponent<BoxColliderComponent>();	

				for (var j = i; j < entities.Count; j++)
				{
					Entity b = entities[j];

					if (a == b)
						continue;

					var bTransform = b.GetComponent<TransformComponent>();	
					var bCollider = b.GetComponent<BoxColliderComponent>();

					bool collisionHappened = aCollider.Bounds.Intersects(bCollider.Bounds);

					if (collisionHappened)
					{
						Logger.Info($"Entity {a.GetId} is colliding with Entity {b.GetId}");
						eventBus.EmitEvent(new CollisionEvent(a, b));	
					}
				}
			}
		}

	}
}
