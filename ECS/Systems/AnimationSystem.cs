using Frith.ECS.Components;
using Frith.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class AnimationSystem : EcsSystem
	{
		private AnimationLibrary animationLibrary;
		private string previousAnimation; 
		public AnimationSystem(Game game)
		{
			RequireComponent<AnimationComponent>();
			animationLibrary = game.Services.GetService<AnimationLibrary>();
			previousAnimation = string.Empty;
		}

		public override void Update(GameTime gameTime)
		{
			
			foreach (var entity in GetSystemEntities())
			{
				ref var animationComponent = ref entity.GetComponent<AnimationComponent>();

				var animation = animationLibrary.GetAnimation(animationComponent.AnimationName);

				var animationChanged = animationComponent.AnimationName != previousAnimation;



			
				if (animationChanged)
				{
					animation.Iterator = 0;
					animationComponent.CurrentTime = 0f;
					animationComponent.CurrentFrame = animation.GetFrame(animation.Iterator);
				} 

				animationComponent.CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;


				if (animationComponent.CurrentTime >= animation.FrameDuration)
				{

					animation.Iterator++;

					if (animation.Iterator >= animation.FrameCount)
					{
						if (animation.PlaybackType == AnimationPlaybackType.Looping)
							animation.Iterator = 0;
						else
							animation.Iterator = animation.FrameCount - 1;
					}

					animationComponent.CurrentFrame = animation.GetFrame(animation.Iterator);
					animationComponent.CurrentTime = 0;
				}
				




				previousAnimation = animationComponent.AnimationName;
			
			}
		}
	}
}
