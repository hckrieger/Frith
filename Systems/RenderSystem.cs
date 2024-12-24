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
			RequireComponent<TransformComponent>();
			RequireComponent<SpriteComponent>();

			textureManager = game.Services.GetService<TextureManager>();
        }

		


        public override void Draw(SpriteBatch spriteBatch)
        {
			var textureCache = textureManager.GetAllTextureData();

			foreach (var entity in GetSystemEntities())
			{
				ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
				ref SpriteComponent sprite = ref entity.GetComponent<SpriteComponent>();

		
				TextureData? textureData = textureCache[entity.GetId()];

				
				spriteBatch.Draw(textureData?.Texture, transform.Position, sprite.Rectangle, Color.White, (float)transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, sprite.LayerDepth);
			
				

			}

        }
    }
}
