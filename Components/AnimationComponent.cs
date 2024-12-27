using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
    public struct AnimationComponent(int numFrames = 1, float frameTime = 0, bool isLoop = true)
    {
        public int NumFrames { get; } = numFrames;

        public bool IsPlaying { get; set; } = false;

        public int CurrentFrame { get; set; } = 1;

        public float FrameTime { get; set; } = frameTime;

        public float CurrentTime { get; set; } = 0;

        private bool isLoop = isLoop;
    }
}
