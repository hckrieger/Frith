using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
    public struct KeyboardControlledComponent
    {
       private Vector2 upVelocity, rightVelocity, downVelocity, leftVelocity;

        public Vector2 UpVelocity => upVelocity;

        public Vector2 RightVelocity => rightVelocity;

        public Vector2 DownVelocity => downVelocity;


        public Vector2 LeftVelocity => leftVelocity;    

        public KeyboardControlledComponent(Vector2 upVelocity, Vector2 rightVelocity, Vector2 downVelocity, Vector2 leftVelocity)
        {
            this.upVelocity = upVelocity;
            this.rightVelocity = rightVelocity;
            this.downVelocity = downVelocity;
            this.leftVelocity = leftVelocity;
        }
    }
}
