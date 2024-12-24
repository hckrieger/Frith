using Frith.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
	public class MovementSystem : System
	{

        public MovementSystem()
        {
            RequireComponent<TransformComponent>();
            RequireComponent<RigidBodyComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in GetSystemEntities())
            {
                ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
                ref RigidBodyComponent rigidBody = ref entity.GetComponent<RigidBodyComponent>();

                float xPosition = rigidBody.Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                float yPosition = rigidBody.Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;


				transform.Position += new Vector2(xPosition, yPosition);
			}
        }
    }
}
