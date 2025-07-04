using Frith.ECS.Components;
using Frith.Services;
using Frith.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class RenderBmpTextSystem : EcsSystem
	{
		private readonly AssetCache<Texture2D> textureCache;
		private SpriteBatch spriteBatch;
		public RenderBmpTextSystem(Game game, SpriteBatch spriteBatch)
		{
			RequireComponent<TransformComponent>();
			RequireComponent<BmpTextComponent>();

			textureCache = game.Services.GetService<AssetCache<Texture2D>>();	
			this.spriteBatch = spriteBatch;
		}



		public override void Draw()
		{
			base.Draw();

		

			foreach (var entity in GetSystemEntities())
			{
				ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
				ref BmpTextComponent text = ref entity.GetComponent<BmpTextComponent>();

				Texture2D texture = textureCache.GetAsset(text.TextureName) ?? throw new NullReferenceException("The texture hasn't been added");

				char[] letters = text.Text.ToCharArray();
				int x = 0;
				int y = 0;

				for (int i = 0; i < letters.Length; i++)
				{
					if (letters[i] == '\n')
					{
						y += text.FrameSize.Y;
						x = 0;
						continue;
					}

					Vector2 position = transform.Position + new Vector2(x * text.FrameSize.X, y);
					Rectangle sourceRectangle = SpriteSheetUtils.SourceRectangle(letters[i] - 32, texture, text.FrameSize);

					spriteBatch.Draw(
						texture,
						position,
						sourceRectangle,
						text.Color,
						transform.Rotation,
						Vector2.Zero,
						transform.Scale,
						SpriteEffects.None,
						1f);

					x++;
				}
			}
		}
	}
}
