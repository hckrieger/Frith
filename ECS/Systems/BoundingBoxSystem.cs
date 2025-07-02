using Frith.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class BoundingBoxSystem : EcsSystem
	{

		public BoundingBoxSystem()
		{
			RequireComponent<BoxColliderComponent>();
			RequireComponent<TransformComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			foreach (var entity in GetSystemEntities())
			{
				ref BoxColliderComponent boxColliderComponent = ref entity.GetComponent<BoxColliderComponent>();
				ref TransformComponent transformComponent = ref entity.GetComponent<TransformComponent>();

				boxColliderComponent.Bounds = new Rectangle((int)(transformComponent.Position.X + boxColliderComponent.LocalBounds.X), 
					                                        (int)(transformComponent.Position.Y + boxColliderComponent.LocalBounds.Y), 
															boxColliderComponent.LocalBounds.Width, boxColliderComponent.LocalBounds.Height);
			}
		}
		
	}
}
