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

namespace Frith
{
    public class Engine : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		protected SpriteBatch spriteBatch => _spriteBatch;

		Texture2D texture;

		private RenderTarget2D renderTarget;

		protected Point internalResolution;
		protected Point screenSize;

		protected DisplayManager displayManager;
		protected Registry registry;
		protected AssetStore assetStore;
		protected bool isDebug;
		protected EventBus eventBus;
		protected Rectangle camera;

		private KeyboardState currentKeyboardState, previousKeyboardState;

        public Engine()
        {
			_graphics = new GraphicsDeviceManager(this);
			
			registry = new Registry();
			assetStore = new AssetStore(Content);
			isDebug = false;
			eventBus = new EventBus();	

			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_graphics.DeviceResetting += (sender, e) =>
			{
				UpdateRenderTarget();
			};

			

			
		}

		protected bool isFullScreen
		{
			get
			{
				return _graphics.IsFullScreen;
			}

			set
			{
				_graphics.IsFullScreen = value;
				displayManager.SetScreenSize();
				
			}
		}

		private void UpdateRenderTarget()
		{
			if (renderTarget != null)
			{
				renderTarget.Dispose();
			}

			renderTarget = new RenderTarget2D(GraphicsDevice, displayManager.InternalResolution.X, displayManager.InternalResolution.Y);
		}

		protected void SetResolution(int resolutionWidth, int resolutionHeight, float scale = 1)
		{
			displayManager.InternalResolution = new Point(resolutionWidth, resolutionHeight);

			float width = resolutionWidth * scale;
			float height = resolutionHeight * scale;

			displayManager.WindowSize = new Point((int)width, (int)height);

				UpdateRenderTarget();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			displayManager = new DisplayManager(_graphics);

			AddServices();

			
			//UpdateRenderTarget();
		}

		protected override void LoadContent()
		{

			
			texture = Content.Load<Texture2D>("batman");

			// TODO: use this.Content to load your game content here
		}

		

		private void AddServices()
		{
			Dictionary<Type, object> addedServices = new Dictionary<Type, object>
			{
				{ typeof(DisplayManager), displayManager},
				{ typeof(Registry), registry },
			};

			foreach (var service in addedServices.Values)
			{
				Services.AddService(service.GetType(), service);
			}

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			//	previousKeyboardState = Keyboard.GetState();

			currentKeyboardState = Keyboard.GetState();

			if (currentKeyboardState.IsKeyDown(Keys.Tab) && !previousKeyboardState.IsKeyDown(Keys.Tab))
			{
				isDebug = !isDebug;

				if (isDebug)
					Logger.Info("Debugger is turned on");
				else
					Logger.Info("Debugger is off");


			}

			

			foreach (var key in currentKeyboardState.GetPressedKeys())
			{
				
				if (currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key))
				{

					var eventKey = $"{key}";

					if (!eventBus.EventCache.TryGetValue(eventKey, out Event? keyboardEvent))
					{
						keyboardEvent = new KeyPressedEvent(key);
						eventBus.EventCache[eventKey] = keyboardEvent;
					}
					eventBus.EmitEvent(eventBus.EventCache[eventKey]);
				}
			}

			previousKeyboardState = currentKeyboardState;
			

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);


			_graphics.GraphicsDevice.SetRenderTarget(renderTarget);

			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin();

			registry.GetSystem<RenderSystem>()?.Draw(_spriteBatch, assetStore, camera);
			//_spriteBatch.Draw(texture, Vector2.Zero, Color.White);
			_spriteBatch.End();

			_graphics.GraphicsDevice.SetRenderTarget(null);

			// TODO: Add your drawing code here
			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(renderTarget, displayManager.DestinationRectangle, Color.White);
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
