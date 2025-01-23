using Frith.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frith.Systems
{
	public class RenderColliderSystem : System
	{
		private GraphicsDevice graphicsDevice;
        public RenderColliderSystem(GraphicsDevice graphicsDevice)
        {
            RequireComponent<TransformComponent>();
			RequireComponent<BoxColliderComponent>();

			this.graphicsDevice = graphicsDevice;
        }

        private Texture2D HitBoxPixels(Rectangle rectangle)
		{
			Texture2D pixelTexture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);
			Color[] color = new Color[rectangle.Width * rectangle.Height];


			for (int i = 0; i < rectangle.Width * rectangle.Height; i++)
			{
				color[i] = Color.Transparent;

				if (i <= rectangle.Width - 1 ||
					i > (rectangle.Height * rectangle.Width) - rectangle.Width ||
					i % rectangle.Width == 0 ||
					i % rectangle.Width == rectangle.Width - 1)
				{
					color[i] = Color.White;
				}
			}

			pixelTexture.SetData(color);

			return pixelTexture;

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (Entity entity in GetSystemEntities())
			{
				Rectangle entityColliderBounds = entity.GetComponent<BoxColliderComponent>().BoundingBox;
				spriteBatch.Draw(HitBoxPixels(entityColliderBounds), entity.GetComponent<TransformComponent>().Position + new Vector2(entityColliderBounds.X, entityColliderBounds.Y), Color.LimeGreen);
			}
		}
	}





}
