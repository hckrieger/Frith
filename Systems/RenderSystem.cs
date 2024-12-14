using Frith.Components;
using Frith.Managers;
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
		private TextureManager textureManager;

        public RenderSystem(Game game)
        {

        
			textureManager = game.Services.GetService<TextureManager>();
        }

		public void Initialize()
		{
			RequireComponent<TransformComponent>();
			RequireComponent<SpriteComponent>();
		}



        public void Draw(SpriteBatch spriteBatch)
        {
			var textureCache = textureManager.GetAllTextureData();

			foreach (var entity in GetSystemEntities())
			{
				TransformComponent transform = entity.GetComponent<TransformComponent>();
				SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

		
				TextureData? textureData = textureCache[entity.GetId()];

				

				spriteBatch.Draw(textureData?.Texture, transform.Position, sprite.Rectangle, Color.White, (float)transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, sprite.LayerDepth);
			
				

			}

        }
    }
}
