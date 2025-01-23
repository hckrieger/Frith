using Frith.Components;
using Microsoft.Xna.Framework;

namespace Frith.Systems
{
	public class AnimationSystem : System
	{
        public AnimationSystem()
        {
            RequireComponent<AnimationComponent>();
            RequireComponent<SpriteComponent>();

        }


        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Entity entity in GetSystemEntities())
            {
                ref var animation = ref entity.GetComponent<AnimationComponent>();
                ref var sprite = ref entity.GetComponent<SpriteComponent>();

				animation.CurrentTime += deltaTime;

				if (animation.CurrentTime >= animation.FrameTime)
                {
                    animation.CurrentFrame = (int)(animation.CurrentFrame + 1) % animation.NumFrames;

                    animation.CurrentTime -= animation.FrameTime;

                   // sprite.SrcRectX = animation.CurrentFrame * sprite.Width;
                }



               
            }
        }
    }
}
