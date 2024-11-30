using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
    public class DisplayManager
    {
        private GraphicsDeviceManager graphics;

        private Point internalResolution;
        public Point InternalResolution
        {
            get { return internalResolution; }
            set {
                
                internalResolution = value; 
            }
        }

        public Point WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
                SetScreenSize();
            }
        }

        private Point windowSize;

        private Rectangle destinationRectangle;

        public Rectangle DestinationRectangle => destinationRectangle;
        public DisplayManager(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            
        }


        public void SetScreenSize()
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
			SetDestinationRectangle();
            
        }


        private void SetDestinationRectangle()
        {
            float screenAspectRatio = (float)graphics.GraphicsDevice.PresentationParameters.BackBufferWidth / graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;
            float resolutionAspectRatio = (float)internalResolution.X / internalResolution.Y;
            int x, y, width, height;

            if (screenAspectRatio > resolutionAspectRatio)
            {

                height = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;
                width = (int)(height * resolutionAspectRatio);
                x = (graphics.GraphicsDevice.PresentationParameters.BackBufferWidth - width) / 2;
                y = 0;
                
            } else
            {
                width = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
                height = (int)(width / resolutionAspectRatio);
                y = (graphics.GraphicsDevice.PresentationParameters.BackBufferHeight - height) / 2;
                x = 0;
            }

            destinationRectangle = new Rectangle(x, y, width, height);  
        }

        public Vector2 ScreenToViewport(Vector2 position)
        {

            var scaleX = (float)destinationRectangle.Width / internalResolution.X;
            var scaleY = (float)destinationRectangle.Height / internalResolution.Y;

            var x = (position.X - destinationRectangle.X) / scaleX;
            var y = (position.Y - destinationRectangle.Y) / scaleY;

            x = (int)Math.Round(x);
            y = (int)Math.Round(y);

            return new Vector2(x, y);
        }
    }
}
