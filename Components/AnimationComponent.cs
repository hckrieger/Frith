using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
    public struct AnimationComponent
    {
        private int numFrames;
        public int NumFrames =>  numFrames;

        public bool IsPlaying { get; set; } = false;

        private int currentFrame;

        public int CurrentFrame
        {
            get {
                return currentFrame;
            }

            set
            {
                currentFrame = value;
            }
        }

        private float frameTime;

        public float FrameTime
        {
            get => frameTime;
            set { frameTime = value; }
        }

        public float CurrentTime { get; set; } = 0;

        private bool isLoop;

        public AnimationComponent(int numFrames = 1, float frameTime = 0, bool isLoop = true)
        {
            this.numFrames = numFrames;
            this.frameTime = frameTime;
            this.isLoop = isLoop;
            this.currentFrame = 1;
            
        }
    }
}
