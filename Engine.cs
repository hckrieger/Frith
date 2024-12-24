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
		private GraphicsDeviceManager _graphics;
		protected SpriteBatch? _spriteBatch;

		//protected AssetStore<Texture2D> textureAssets;
		//protected AssetStore<SpriteFont> TtfFontAssets;

		private RenderTarget2D? renderTarget;
		protected GraphicalAssetManager graphicalAssetManager;

		protected Point internalResolution;
		protected Point screenSize;

		protected DisplayManager displayManager;
		protected TextureManager textureManager;

		protected SceneManager sceneManager;

		protected InputManager inputManager;
		
		
		protected bool isDebug;
		protected EventBus eventBus;

		private KeyboardState currentKeyboardState, previousKeyboardState;

        public Engine()
        {
			_graphics = new GraphicsDeviceManager(this);
			
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
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			inputManager = new InputManager(this, displayManager);
			sceneManager = new SceneManager(_spriteBatch, this);

			Components.Add(inputManager);
			Components.Add(sceneManager);

			AddServices();

			SetResolution(720, 480);
			
			UpdateRenderTarget();

			base.Initialize();
		}


		

		private void AddServices()
		{
			Dictionary<Type, object> addedServices = new Dictionary<Type, object>
			{
				{ typeof(DisplayManager), displayManager},
				{ typeof(GraphicalAssetManager), graphicalAssetManager },
				{ typeof(TextureManager), textureManager },
				{ typeof(InputManager), inputManager },
				{ typeof(SceneManager), sceneManager },	

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
			Globals<Registry>.Instance().Update();


			base.Update(gameTime);


			previousKeyboardState = currentKeyboardState;
			
		}

		protected override void Draw(GameTime gameTime)
		{


			_graphics.GraphicsDevice.SetRenderTarget(renderTarget);

			GraphicsDevice.Clear(Color.Black);

			if (_spriteBatch == null)
			{
				throw new Exception("Spritebatch cannot be null");
			}
				

			_spriteBatch.Begin();

			

				base.Draw(gameTime);

			//Globals<Registry>.Instance().GetSystem<RenderSystem>();
			//Globals<Registry>.Instance().GetSystem<RenderBmpTextSystem>();

			_spriteBatch.End();

			_graphics.GraphicsDevice.SetRenderTarget(null);

			// TODO: Add your drawing code here
			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(renderTarget, displayManager.DestinationRectangle, Color.White);
			_spriteBatch.End();

		}
	}
}
