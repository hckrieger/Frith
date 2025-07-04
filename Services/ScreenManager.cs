using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{

	public class Screen
	{

		public virtual void Initialize()
		{

		}

		public virtual void OnEnter()
		{

		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw()
		{

		}
	}

	public class ScreenManager(Game game) : DrawableGameComponent(game)
	{

		private Screen? currentScreen;

		private Dictionary<string, Screen> screens = new Dictionary<string, Screen>();

		public void AddScreen(string screenName, Screen screen, bool makeActiveScreen = false)
		{
			if (!screens.ContainsKey(screenName))
			{
				screens.Add(screenName, screen);

				screen.Initialize();

				if (makeActiveScreen)
					SwitchToScreen(screenName);
			}
			else
				throw new Exception($"Can't add {screenName} because it already exists");




			
		}

		public void SwitchToScreen(string screenName)
		{
			if (screens.TryGetValue(screenName, out Screen? screen))
			{
				currentScreen = screen;
				currentScreen.OnEnter();
			}
			else
				throw new NullReferenceException($"the screen name {screenName} doesn't exist");
			
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			currentScreen?.Update(gameTime);
		}


		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			currentScreen?.Draw();
		}

	}
}
