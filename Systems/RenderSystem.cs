using Frith.Components;
using Frith.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
				ref SpriteComponent sprite = ref entity.GetComponent<SpriteComponent>();
				ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
				

		
				TextureData? textureData = textureCache[entity.GetId()];

				if (sprite.Visible)
					spriteBatch.Draw(textureData?.Texture, transform.Position, sprite.Rectangle, sprite.Color, (float)transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, sprite.LayerDepth);
				
				

			}

        }
    }
}
