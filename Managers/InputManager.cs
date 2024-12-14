﻿using Microsoft.Xna.Framework;
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

		private MouseState previousMouseState;
		private MouseState currentMouseState;

		public MouseState PreviousMouseState => previousMouseState;

		public KeyboardState previousKeyboardState;
		private KeyboardState currentKeyboardState;

		private DisplayManager displayerManager;

		public InputManager(Game game, DisplayManager displayManager) : base(game) 
		{
			this.displayerManager = displayManager;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

		}

		public bool KeyJustDown(Keys key)
		{
			return (previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key));	
		}

		public bool KeyHeld(Keys key)
		{
			return currentKeyboardState.IsKeyDown(key);
		}

		public bool MouseButtonJustDown()
		{
			return (previousMouseState.LeftButton == ButtonState.Released) &&
				   (currentMouseState.LeftButton == ButtonState.Pressed);

		}

		public bool MouseButtonHeld()
		{
			return currentMouseState.LeftButton == ButtonState.Pressed;
		}


	

		public Vector2 MousePosition()
		{
			return displayerManager.ScreenToViewport(currentMouseState.Position.ToVector2());
		}
	}
}
