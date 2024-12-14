using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class InputManager : GameComponent
	{

		private MouseState previousMouseState, currentMouseState;


		public InputManager(Game game) : base(game) 
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();
			
		}

		public bool MouseButtonJustDown()
		{
			if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
			{
				return true;
			}

			return false;	
		}

		public bool MouseButtonHeld()
		{
			if (currentMouseState.LeftButton == ButtonState.Pressed)
			{
				return true;
			}

			return false;
		}


		public Point MousePositionPoint()
		{
			return currentMouseState.Position;
		}

		public Vector2 MousePositionVector()
		{
			return new Vector2(currentMouseState.X, currentMouseState.Y);
		}
	}
}
