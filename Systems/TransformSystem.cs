using Frith.Components;
using Frith.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledLib;

namespace Frith.Systems
{
    public class TransformSystem : System
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

                if (entityTransform.Parent != null)
                {
                    ref var parentTransform = ref entityTransform.Parent.GetComponent<TransformComponent>();

                    entityTransform.Position = parentTransform.Position + entityTransform.LocalPosition;

                    entityTransform.HadParentInLastFrame = true;
                } else
                {
                    if (entityTransform.HadParentInLastFrame)
                    {
                        entityTransform.LocalPosition = entityTransform.Position;
                        entityTransform.HadParentInLastFrame = false;
                    }

                    entityTransform.Position = entityTransform.LocalPosition;


                }

                
            }
      
        }
    }
}