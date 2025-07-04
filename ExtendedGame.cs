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
		private InputManager inputManager;
		private CameraManager cameraManager;
		private TiledMapManager tiledMapManager;
		private AnimationLibrary animationLibrary;
		protected ScreenManager screenManager;

		
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

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
		
			

			

			Services.AddService(displayManager);
			Services.AddService(textureCache);
			Services.AddService(tilemapCache);
			Services.AddService(inputManager);
			Services.AddService(cameraManager);
			Services.AddService(tiledMapManager);
			Services.AddService(animationLibrary);
			Services.AddService(screenManager);

			Components.Add(inputManager);
			Components.Add(screenManager);

			base.Initialize();
		}


		protected override void LoadContent()
		{
			

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			foreach (var system in registry.GetAllSystems())
			{
				system.Update(gameTime);
			}

			// TODO: Add your update logic here
			registry.Update();



			base.Update(gameTime);
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			base.Draw(gameTime);

			GraphicsDevice.SetRenderTarget(renderTarget);

			_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: cameraManager.TransformMatrix);


				foreach (var system in registry.GetAllSystems())
				{
					system.Draw();
				}

			_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);



			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);

				_spriteBatch.Draw(renderTarget, displayManager.Viewport.Bounds, Color.White);
			
			_spriteBatch.End();




			
		}
	}
}
