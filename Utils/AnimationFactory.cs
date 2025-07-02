using Frith.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Utils
{
	public class AnimationFactory
	{
		public static void AddAnimation(
			string name,
			int[] frameIndices,
			Texture2D texture,
			Point frameSize,
			AnimationPlaybackType playbackType,
			AnimationLibrary animationLibrary,
			float frameDuration = 0.25f)
		{
			var frames = SpriteSheetUtils.GenerationAnimationFrames(frameIndices, texture, frameSize);
			var animation = new Animation(name, frames, frameDuration, playbackType);
			animationLibrary.AddAnimation(name, animation);
		}
	}
}
