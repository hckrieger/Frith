using Frith.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class TransformSystem : EcsSystem
	{

		public TransformSystem()
		{
			RequireComponent<TransformComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			foreach (var entity in GetSystemEntities())
			{
				ref var entityTransform = ref entity.GetComponent<TransformComponent>();

				if (entityTransform.Parent == null)
				{
					if (entityTransform.HadParentInLastFrame)
					{
						entityTransform.LocalPosition = entityTransform.Position;
						entityTransform.LocalRotation = entityTransform.Rotation;
						entityTransform.LocalScale = entityTransform.Scale;

						entityTransform.HadParentInLastFrame = false;
					}

					entityTransform.Position = entityTransform.LocalPosition;
					entityTransform.Rotation = entityTransform.LocalRotation;
					entityTransform.Scale = entityTransform.LocalScale;
				} else
				{
					ref var parentTransform = ref entityTransform.Parent.GetComponent<TransformComponent>();

					entityTransform.Position = parentTransform.Position + entityTransform.LocalPosition;
					entityTransform.Rotation = parentTransform.Rotation + entityTransform.LocalRotation;
					entityTransform.Scale = parentTransform.Scale * entityTransform.LocalScale;

					entityTransform.HadParentInLastFrame = true;
				}
			
				
			}
		}
	}
}
