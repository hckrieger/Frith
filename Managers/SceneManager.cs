using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frith.Managers
{
	public class SceneManager(SpriteBatch spriteBatch, Game game) : DrawableGameComponent(game)
	{

		private readonly Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();

		private Scene? currentScene;


		public void AddScene(string sceneName, Scene scene)
		{
			if (scenes.TryAdd(sceneName, scene))
			{
				scene.OnCreate();

			}
		}

		public void SwitchToScene(string sceneName)
		{
			if (!scenes.TryGetValue(sceneName, out var newScene)) return;
			
			currentScene?.OnExit();
			currentScene = newScene;

			if (!currentScene.HasBeenVisited)
				currentScene?.OnFirstEnter();

			currentScene?.OnEnter();
		}

		public Scene? GetScene(string sceneName)
		{
			return scenes.GetValueOrDefault(sceneName);
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
