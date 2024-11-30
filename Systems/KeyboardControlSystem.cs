using Frith.Components;
using Frith.Events;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
	public class KeyboardControlSystem : System
	{

        public KeyboardControlSystem()
        {
            RequireComponent<KeyboardControlledComponent>();
            RequireComponent<SpriteComponent>();   
            RequireComponent<RigidBodyComponent>(); 
        }

        public void SubscribeToEvents(EventBus eventBus)
        {
            eventBus.SubscribeToEvent<KeyPressedEvent>(OnKeyPressed);
        }

        public void OnKeyPressed(KeyPressedEvent keyEvent) {
            foreach (var entity in GetSystemEntities())
            {
                ref var keyboardControl = ref entity.GetComponent<KeyboardControlledComponent>();
                ref var sprite = ref entity.GetComponent<SpriteComponent>();
                ref var rigidbody = ref entity.GetComponent<RigidBodyComponent>();

                switch (keyEvent.Key)
                {
                    case Keys.Up:
                        rigidbody.Velocity = keyboardControl.UpVelocity;
                        sprite.SrcRectY = sprite.Height * 0;
                        break;
					case Keys.Right:
						rigidbody.Velocity = keyboardControl.RightVelocity;
						sprite.SrcRectY = sprite.Height * 1;
						break;
					case Keys.Down:
                        rigidbody.Velocity = keyboardControl.DownVelocity;
                        sprite.SrcRectY = sprite.Height * 2;
                        break;
                    case Keys.Left:
                        rigidbody.Velocity = keyboardControl.LeftVelocity;
						sprite.SrcRectY = sprite.Height * 3;
						break;

                }
            }
        }

        public void Update()
        {

        }
    }
}
