using Frith.Components;
using Frith.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Frith.Systems
{
	public class RenderBmpTextSystem : System
	{
		private TextureManager textureManager;
		public RenderBmpTextSystem(Game game) 
		{
			RequireComponent<TransformComponent>();
			RequireComponent<TextLabelComponent>();

			textureManager = game.Services.GetService<TextureManager>();

		}

	


		public override void Draw(SpriteBatch spriteBatch)
		{
			var textureCache = textureManager.GetAllTextureData();

			foreach (var entity in GetSystemEntities())
			{
				ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
				ref TextLabelComponent textLabel = ref entity.GetComponent<TextLabelComponent>();

				TextureData? textureData = textureCache[textLabel.TextureId];
				int xPositionShift = 0;
				int yPositionShift = 0;

				for (int i = 0; i < textLabel.Text.Length; i++)
				{
					int index = textLabel.TextCharacters[i] - 32;


					if (textureData != null)
						xPositionShift += textureData.FrameSize.X;
					

					if (textLabel.TextCharacters[i] == '\n' && textureData != null)
					{
						xPositionShift = 0;
						yPositionShift = textureData.FrameSize.Y + (textureData.FrameSize.Y / 8);
						continue;
					}
					


					spriteBatch.Draw(textureData?.Texture, transform.Position + new Vector2(xPositionShift, yPositionShift), textureData?.GetTextureFrame(index), textLabel.Color, (float)transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, .75f);

				}




			}

		}
	}
}
