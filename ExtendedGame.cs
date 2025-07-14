using Frith.ECS;
using Frith.ECS.Systems;
using Frith.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Frith.Services.TiledMapManager;
using System.Text.Json.Serialization;
using System.ComponentModel;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.CompilerServices;

namespace Frith
{
	public class ExtendedGame : Game
	{
		protected GraphicsDeviceManager _graphics;
		protected SpriteBatch _spriteBatch;
		private RenderTarget2D? renderTarget;
		private DisplayManager displayManager;
		private AssetCache<Texture2D> textureCache;
		private AssetCache<TiledMap?> tilemapCache;
		private AssetCache<Song> songCache;
		private AssetCache<SoundEffect> soundEffectCache;
		private InputManager inputManager;
		private CameraManager cameraManager;
		private TiledMapManager tiledMapManager;
		private AnimationLibrary animationLibrary;
		private EventBus eventBus;
		protected ScreenManager screenManager;
		protected bool useCamera = false;
		

		
		protected Registry registry = new Registry();


		public ExtendedGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			_graphics.HardwareModeSwitch = true;
			_graphics.ApplyChanges();

			_spriteBatch = new SpriteBatch(GraphicsDevice);
			displayManager = new DisplayManager(_graphics, UpdateRenderTarget);
			

			textureCache = new AssetCache<Texture2D>(a => Content.Load<Texture2D>(a));
			songCache = new AssetCache<Song>(a => Content.Load<Song>(a));
			soundEffectCache = new AssetCache<SoundEffect>(a => Content.Load<SoundEffect>(a));
			tilemapCache = new AssetCache<TiledMap?>(a =>
			{
				string json = File.ReadAllText($"Content/{a}");
				return JsonSerializer.Deserialize<TiledMap>(json);
			});




			inputManager = new InputManager(this, displayManager);
			cameraManager = new CameraManager();
			animationLibrary = new AnimationLibrary();
			screenManager = new ScreenManager(this);

			tiledMapManager = new TiledMapManager()
			{
				TiledMapCache = tilemapCache
			};

			eventBus = new EventBus();	
		}


	

		protected void SetResolution(Point resolution, int scale = 1)
		{
			displayManager.InternalResolution = new Point(resolution.X, resolution.Y);
			displayManager.WindowSize = new Point(resolution.X * scale, resolution.Y * scale);

			if (cameraManager != null && cameraManager?.VisibleCameraArea == Rectangle.Empty)
			{
				cameraManager.VisibleCameraArea = new Rectangle(0, 0, resolution.X, resolution.Y);
			}

			UpdateRenderTarget();
		}


		private void UpdateRenderTarget()
		{

			if (displayManager != null)
			{
				renderTarget?.Dispose();
				renderTarget = new RenderTarget2D(GraphicsDevice, displayManager.InternalResolution.X, displayManager.InternalResolution.Y);
			}
				
		}

		private void ServiceRegistration()
		{
			List<(Type, object)> servicesToAdd = new List<(Type, object)>()
			{
				(typeof(DisplayManager), displayManager),
				(typeof(AssetCache<Texture2D>), textureCache),
				(typeof(AssetCache<TiledMap>), tilemapCache),
				(typeof(InputManager), inputManager),
				(typeof(CameraManager), cameraManager),
				(typeof(TiledMapManager),  tiledMapManager),
				(typeof(AnimationLibrary), animationLibrary),
				(typeof(ScreenManager), screenManager),
				(typeof(AssetCache<Song>), songCache),
				(typeof(AssetCache<SoundEffect>), soundEffectCache),
				(typeof(EventBus), eventBus),	
			};

			foreach (var (type, instance) in servicesToAdd)
				Services.AddService(type, instance);
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			ServiceRegistration();
			

			Components.Add(inputManager);
			Components.Add(screenManager);

			base.Initialize();
		}


	

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			foreach (var system in registry.GetAllSystems())
				system.Update(gameTime);
			

			// TODO: Add your update logic here
			registry.Update();



			base.Update(gameTime);
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			

			GraphicsDevice.SetRenderTarget(renderTarget);

			Matrix matrix = useCamera ? cameraManager.TransformMatrix : Matrix.Identity;

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);


				foreach (var system in registry.GetAllSystems())
					system.Draw();

				base.Draw(gameTime);

			_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);



			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);

				_spriteBatch.Draw(renderTarget, displayManager.Viewport.Bounds, Color.White);
			
			_spriteBatch.End();




			
		}
	}
}
