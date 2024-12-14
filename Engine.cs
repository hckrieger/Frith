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
		private SpriteBatch? _spriteBatch;
		protected SpriteBatch? spriteBatch => _spriteBatch;

		//protected AssetStore<Texture2D> textureAssets;
		//protected AssetStore<SpriteFont> TtfFontAssets;

		private RenderTarget2D? renderTarget;
		protected GraphicalAssetManager graphicalAssetManager;

		protected Point internalResolution;
		protected Point screenSize;

		protected DisplayManager displayManager;
		protected TextureManager textureManager;


		protected Registry registry;
		
		
		protected bool isDebug;
		protected EventBus eventBus;

		private KeyboardState currentKeyboardState, previousKeyboardState;

        public Engine()
        {
			_graphics = new GraphicsDeviceManager(this);
			
			registry = new Registry();
			//textureAssets = new AssetStore<Texture2D>(Content);
			isDebug = false;
			eventBus = new EventBus();	

			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_graphics.DeviceResetting += (sender, e) =>
			{
				UpdateRenderTarget();
			};


			displayManager = new DisplayManager(_graphics);

			graphicalAssetManager = new GraphicalAssetManager(Content);
			textureManager = new TextureManager();

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
			AddServices();

			SetResolution(720, 480);
			
			UpdateRenderTarget();
		}

		protected override void LoadContent()
		{
			


			// TODO: use this.Content to load your game content here
		}

		

		private void AddServices()
		{
			Dictionary<Type, object> addedServices = new Dictionary<Type, object>
			{
				{ typeof(DisplayManager), displayManager},
				{ typeof(Registry), registry },
				{ typeof(GraphicalAssetManager), graphicalAssetManager },
				{ typeof(TextureManager), textureManager}

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
			registry.Update();


				previousKeyboardState = Keyboard.GetState();

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

			if (_spriteBatch == null)
			{
				throw new Exception("Spritebatch cannot be null");
			}
				

			_spriteBatch.Begin();

			registry.GetSystem<RenderSystem>()?.Draw(_spriteBatch);
			registry.GetSystem<RenderBmpTextSystem>()?.Draw(_spriteBatch);
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
