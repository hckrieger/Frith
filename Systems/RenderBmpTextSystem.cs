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

	


		public void Draw(SpriteBatch spriteBatch)
		{
			var textureCache = textureManager.GetAllTextureData();
			int xPositionShift = 0;
			int yPositionShift = 0;
			foreach (var entity in GetSystemEntities())
			{
				TransformComponent transform = entity.GetComponent<TransformComponent>();
				TextLabelComponent textLabel = entity.GetComponent<TextLabelComponent>();

				TextureData? textureData = textureCache[textLabel.AssetId];


				for (int i = 0; i < textLabel.Text.Length; i++)
				{
					int index = textLabel.TextCharacters[i] - 32;


					
					xPositionShift += textureData.FrameSize.X;
					

					if (textLabel.TextCharacters[i] == '\n')
					{
						xPositionShift = 0;
						yPositionShift = textureData.FrameSize.Y + (textureData.FrameSize.Y / 8);	
					}
					


					spriteBatch.Draw(textureData?.Texture, transform.Position + new Vector2(xPositionShift, yPositionShift), textureData?.GetTextureFrame(index), textLabel.Color, (float)transform.Rotation, Vector2.Zero, transform.Scale, SpriteEffects.None, .75f);

				}




			}

		}
	}
}
