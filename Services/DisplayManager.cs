using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class DisplayManager(GraphicsDeviceManager graphics, Action updateRenderTarget)
	{
		/// <summary>
		/// Window size is the size of the window in windowed mode
		/// </summary>
		private Point windowSize;
		public Point WindowSize
		{
			get => windowSize;
			set
			{
				windowSize = value;
				UpdateBackBuffer();
			}
		}

		/// <summary>
		/// Internal resolution is the game resolution that scales to the window size
		/// </summary>
		private Point internalResolution;
		public Point InternalResolution
		{
			get => internalResolution;
			set
			{
				internalResolution = value;
				updateRenderTarget();
			}
		}

		public Viewport Viewport { get; set; }

		/// <summary>
		/// Sets the backbuffer to windowed size or full screen size depending on whether fullscreen is set.
		/// Sets the viewport size, thereafter
		/// </summary>
		private void UpdateBackBuffer()
		{
			
			if (graphics.IsFullScreen)
			{
				graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
				
			} else
			{
				graphics.PreferredBackBufferWidth = windowSize.X;
				graphics.PreferredBackBufferHeight = windowSize.Y;
			}
			graphics.ApplyChanges();

			SetViewportBounds();
		}

		public void ToggleFullScreen()
		{
			graphics.IsFullScreen = !graphics.IsFullScreen;
			UpdateBackBuffer();
		}


		private void SetViewportBounds()
		{
			var screenAspectRatio = (float)windowSize.X / windowSize.Y;
			var internalAspectRatio = (float)InternalResolution.X / InternalResolution.Y;

			var backBufferWidth = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
			var backBufferHeight = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;


			int x, y, width, height;

			if (internalAspectRatio <= screenAspectRatio)
			{
				height = backBufferHeight;
				y = 0;
				width = (int)(height * internalAspectRatio);
				x = (int)(backBufferWidth - width) / 2;
			} else 
			{
				width = backBufferWidth;
				x = 0;
				height = (int)(width / internalAspectRatio);
				y = (int)(backBufferHeight - height) / 2;
			} 


				Viewport = new Viewport(x, y, width, height);
		}

		public Vector2 ScreenToViewport(Vector2 rawVector)
		{
			var scaleX = Viewport.Width / internalResolution.X;
			var scaleY = Viewport.Height / internalResolution.Y;

			var x = (rawVector.X - Viewport.X) / scaleX;
			var y = (rawVector.Y - Viewport.Y) / scaleY;

			x = (int)Math.Round(x);
			y = (int)Math.Round(y);

			return new Vector2(x, y);
		}
	}
}
