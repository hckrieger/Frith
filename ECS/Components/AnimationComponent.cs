using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct AnimationComponent
	{
		public string AnimationName { get; set; }
		//public int CurrentFrameIndex { get; set; }
		public Rectangle CurrentFrame { get; set; }
		public float CurrentTime { get; set; }
		public int MyProperty { get; set; }
		public bool IsPlaying { get; set; }



		public AnimationComponent(string animationName)
		{
			AnimationName = animationName;
			//CurrentFrameIndex = 0;
			CurrentFrame = new Rectangle();
			CurrentTime = 0f;
			IsPlaying = true;
		}
	}
}
