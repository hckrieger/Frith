using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{
	public enum AnimationPlaybackType
	{
		Looping,
		PingPong,
		Terminating
	}

	public class Animation
	{
		public readonly string AnimationName;
		public Rectangle[] Frames { get; set; }
		public float FrameDuration { get; set; }
		public AnimationPlaybackType PlaybackType { get; set; }
		public int FrameCount => Frames.Length;

		public int Iterator { get; set; }

		public Animation(string animationName, Rectangle[] frames, float frameDuration, AnimationPlaybackType playbackType = AnimationPlaybackType.Looping)
		{
			AnimationName = animationName;
			Frames = frames;
			FrameDuration = frameDuration;
			PlaybackType = playbackType;
			Iterator = 0;	
		}

		public Rectangle GetFrame(int index) => Frames[index];
	}

	public class AnimationLibrary
	{
		private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();


		public void AddAnimation(string name, Animation animation)
		{
			if (!animations.TryGetValue(name, out Animation? value))
			{
				animations[name] = animation;
				Logger.Info($"Added animation {name} to animation library");
			}
			else
			{
				Logger.Info($"Tried to add aniation {name} but it was already added");
			}
		}

		public void RemoveAsset(string name)
		{
			if (animations.ContainsKey(name))
			{
				animations?.Remove(name);
			}

		}

		public void ClearAnimations()
		{
			animations.Clear();
		}

		public Animation GetAnimation(string name) 
		{
			if (animations.TryGetValue(name, out Animation? animation))
			{
				return animation;
			}

			throw new Exception($"Animation with asset id {name} doesn't exist in animation library");
		}

	}
}
