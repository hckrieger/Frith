using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class InputManager(Game game, DisplayManager displayManager) : GameComponent(game)
	{
		private KeyboardState previousKeyboardState, currentKeyboardState;
		private MouseState previousMouseState, currentMouseState;

		private DisplayManager displayManager = displayManager;

		public override void Update(GameTime gameTime)
		{
			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();	
		}

		public bool IsKeyDown(Keys key)
		{
			return currentKeyboardState.IsKeyDown(key);
			
		}

		public bool IsKeyPressed(Keys key)
		{
			return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
		}

		public bool IsKeyReleased(Keys key)
		{

			return currentKeyboardState.IsKeyDown(key);
		}

		public bool IsMouseDown()
		{
			return currentMouseState.LeftButton == ButtonState.Pressed;
		}

		public bool IsMousePressed()
		{
			return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
		}

		public Vector2 MousePosition()
		{
			Vector2 vectorPosition = new Vector2(currentMouseState.X, currentMouseState.Y);	
			return displayManager.ScreenToViewport(vectorPosition);
		}
	}
}
