using Frith.Components;
using Frith.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
	public class CollisionSystem : System
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
                Entity entityA = entities[i];

                ref var colliderComponent = ref entityA.GetComponent<BoxColliderComponent>();


             //   if (colliderComponent.BoundingBox == Rectangle.Empty && entityA.HasComponent<SpriteComponent>())
             //   {
                    var spriteComponent = entityA.GetComponent<SpriteComponent>();
                    var transformComponent = entityA.GetComponent<TransformComponent>();

                    colliderComponent.BoundingBox = new Rectangle((int)transformComponent.Position.X, (int)transformComponent.Position.Y, spriteComponent.Rectangle.Width, spriteComponent.Rectangle.Height);
              //  }
                


				// for (int j = i; j < entities.Count; j++)
                // {
                //     Entity entityB = entities[j];

                //     if (entityA == entityB)
                //         continue;

					
				// 	Rectangle entityBBounds = entityB.GetComponent<BoxColliderComponent>().BoundingBox;
				// 	entityBBounds.Offset(entityB.GetComponent<TransformComponent>().Position);

				// 	if (entityABounds.Intersects(entityBBounds))
                //     {
                //         var key = $"{entityA}, {entityB}";

				// 		Logger.Info("Entity " + entityA.GetId() + " is colliding with entity " + entityB.GetId());

                //         if (!eventBus.EventCache.TryGetValue(key, out Event? collisionEvent))
                //         {
                //             collisionEvent = new CollisionEvent(entityA, entityB);
                //             eventBus.EventCache[key] = collisionEvent;
                //         }

                //         eventBus.EmitEvent(eventBus.EventCache[key]);
                //     }
                // }
            }


         }


    }
}
