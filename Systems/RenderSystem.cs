using Frith.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
	public class RenderSystem : System
	{
        public RenderSystem(Game game)
        {
            RequireComponent<TransformComponent>();
            RequireComponent<SpriteComponent>();
            
        }



        public void Draw(SpriteBatch spriteBatch, AssetStore assetStore)
        {
			foreach (var entity in GetSystemEntities())
			{
				TransformComponent transform = entity.GetComponent<TransformComponent>();
				SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

				

                Texture2D? texture = assetStore.GetTexture(sprite.AssetId);

                if (texture != null)
                {
					if (sprite.Width == 0 && sprite.Height == 0)
					{
						sprite.Width = texture.Width;
						sprite.Height = texture.Height;
						sprite.SourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
					}

					Rectangle destinationRectangle =
						new Rectangle(
							(int)transform.Position.X, 
							(int)transform.Position.Y,
							(int)(sprite.Width * transform.Scale.X),
							(int)(sprite.Height * transform.Scale.Y));

					spriteBatch.Draw(texture, destinationRectangle, sprite.SourceRectangle, Color.White, (float)transform.Rotation, Vector2.Zero, SpriteEffects.None, sprite.LayerDepth);

				}

			}

        }
    }
}
