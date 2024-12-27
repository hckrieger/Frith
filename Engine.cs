using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frith.Systems;
using System.Diagnostics;
using Frith.Events;
using Frith.Managers;
using System.Runtime.CompilerServices;

namespace Frith
{
    public class Engine : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch? spriteBatch;


		private RenderTarget2D? renderTarget;
		private readonly GraphicalAssetManager? graphicalAssetManager;

		// protected Point internalResolution;
		// protected Point screenSize;

		private DisplayManager? displayManager;
		private readonly TextureManager? textureManager;

		private SceneManager? sceneManager;

		private InputManager? inputManager;
		
		


		protected Engine()
        {
			graphics = new GraphicsDeviceManager(this);
			
			graphicalAssetManager = new GraphicalAssetManager(Content);
			textureManager = new TextureManager();

			
		

			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			graphics.DeviceResetting += (sender, e) =>
			{
				UpdateRenderTarget();
			};


		}

		protected bool isFullScreen
		{
			get => graphics.IsFullScreen;

			set
			{
				graphics.IsFullScreen = value;
				displayManager?.SetScreenSize();
				
			}
		}

		private void UpdateRenderTarget()
		{
			if (renderTarget != null)
			{
				renderTarget.Dispose();
				renderTarget = null;
			}

			if (displayManager != null)
				renderTarget = new RenderTarget2D(GraphicsDevice, displayManager.InternalResolution.X,
					displayManager.InternalResolution.Y);
		}

		protected void SetResolution(int resolutionWidth, int resolutionHeight, float scale = 1)
		{
			if (displayManager != null)
			{
				displayManager.InternalResolution = new Point(resolutionWidth, resolutionHeight);

				var width = resolutionWidth * scale;
				var height = resolutionHeight * scale;

				displayManager.WindowSize = new Point((int)width, (int)height);
			}

			UpdateRenderTarget();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			spriteBatch = new SpriteBatch(GraphicsDevice);
			displayManager = new DisplayManager(graphics);
			inputManager = new InputManager(this, displayManager);
			sceneManager = new SceneManager(spriteBatch, this);

			Components.Add(inputManager);
			Components.Add(sceneManager);

			AddServices();

			SetResolution(720, 480);
			
			UpdateRenderTarget();

			base.Initialize();
		}


		

		private void AddServices()
		{
			var addedServices = new Dictionary<Type, object?>
			{
				{ typeof(DisplayManager), displayManager},
				{ typeof(GraphicalAssetManager), graphicalAssetManager },
				{ typeof(TextureManager), textureManager },
				{ typeof(InputManager), inputManager },
				{ typeof(SceneManager), sceneManager },	

			};

			foreach (var service in addedServices.Values)
			{
				Services.AddService(service?.GetType(), service);
			}

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			Globals<Registry>.Instance().Update();


			base.Update(gameTime);


			
		}

		protected override void Draw(GameTime gameTime)
		{


			graphics.GraphicsDevice.SetRenderTarget(renderTarget);

			GraphicsDevice.Clear(Color.Black);

			if (spriteBatch == null)
			{
				throw new Exception("Spritebatch cannot be null");
			}
				

			spriteBatch.Begin();

			

				base.Draw(gameTime);

			//Globals<Registry>.Instance().GetSystem<RenderSystem>();
			//Globals<Registry>.Instance().GetSystem<RenderBmpTextSystem>();

			spriteBatch.End();

			graphics.GraphicsDevice.SetRenderTarget(null);

			// TODO: Add your drawing code here
			spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			if (displayManager != null)
				spriteBatch.Draw(renderTarget, displayManager.DestinationRectangle, Color.White);
			spriteBatch.End();

		}
	}
}
