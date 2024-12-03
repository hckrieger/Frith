using Frith.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Systems
{
	public class RenderTextSystem : System
	{
		public RenderTextSystem()
		{
			RequireComponent<TextLabelComponent>();
		}

		public void Draw(SpriteBatch spriteBatch, AssetStore assetStore)
		{
			foreach (var entity in GetSystemEntities())
			{
				var textLabel = entity.GetComponent<TextLabelComponent>();
				SpriteFont? spriteFont = assetStore.GetFont("startText");
				spriteBatch.DrawString(spriteFont, textLabel.Text, textLabel.Position, textLabel.Color);
			}
		}
	}
}
