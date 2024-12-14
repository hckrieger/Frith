using Frith.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frith.Managers;

namespace Frith.Systems
{
	//public class RenderTtfTextSystem : System
	//{
	//	private GraphicalAssetManager graphicalAssetManager;
	//	public RenderTtfTextSystem(Game game)
	//	{
	//		RequireComponent<TextLabelComponent>();
	//		graphicalAssetManager = game.Services.GetService<GraphicalAssetManager>();
	//	}

	//	public void Draw(SpriteBatch spriteBatch)
	//	{
	//		foreach (var entity in GetSystemEntities())
	//		{
	//			var textLabel = entity.GetComponent<TextLabelComponent>();
	//			SpriteFont? spriteFont = graphicalAssetManager.GetSpriteFontTTF(textLabel.AssetId);
	//			//spriteBatch.DrawString(spriteFont, textLabel.Text, textLabel.Position, textLabel.Color);
	//		}
	//	}
	//}
}
