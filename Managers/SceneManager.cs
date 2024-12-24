using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class SceneManager : DrawableGameComponent
	{

		private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

		private Scene? currentScene; 
		private SpriteBatch spriteBatch;

		public SceneManager(SpriteBatch spriteBatch, Game game) : base(game)
		{
			this.spriteBatch = spriteBatch;
		}


		public void AddScene(string sceneName, Scene scene)
		{
			if (!scenes.ContainsKey(sceneName))
			{
				scenes[sceneName] = scene;
				scene.OnCreate();

			}
		}

		public void SwitchToScene(string sceneName)
		{
			if (scenes.TryGetValue(sceneName, out var newScene))
			{
				 currentScene?.OnExit();
				currentScene = newScene;

				if (!currentScene.HasBeenVisited)
					currentScene?.OnFirstEnter();

				currentScene?.OnEnter();
			}
		}

		public Scene? GetScene(string sceneName)
		{
			if (scenes.TryGetValue(sceneName, out Scene? scene))
			{
				return scene;
			}

			return null;
		}




		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);	

			currentScene?.Update(gameTime);	
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			currentScene?.Draw(spriteBatch);
		}
	}
}
