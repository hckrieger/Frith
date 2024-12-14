using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class GraphicalAssetManager
	{

		private readonly Dictionary<string, Texture2D> textureAssets = new Dictionary<string, Texture2D>();
		private readonly Dictionary<string, SpriteFont> ttfTextAssets = new Dictionary<string, SpriteFont>();

		private readonly ContentManager content;

		public GraphicalAssetManager(ContentManager content)
		{
			this.content = content;
		}

		public void AddTexture(string assetId, string filePath)
		{
			if (!textureAssets.ContainsKey(assetId))
			{

				var texture = content.Load<Texture2D>(filePath);

				textureAssets[assetId] = texture;
			}
		}

		public Texture2D GetTexture(string assetId)
		{
			if (textureAssets.TryGetValue(assetId, out Texture2D texture))
			{
				return texture;
			}

			return null;
		}


		public void AddSpriteFontTTF(string assetId, string filePath)
		{
			if (!textureAssets.ContainsKey(assetId))
			{

				var texture = content.Load<SpriteFont>(filePath);

				ttfTextAssets[assetId] = texture;
			}
		}

		public SpriteFont? GetSpriteFontTTF(string assetId)
		{
			if (ttfTextAssets.TryGetValue(assetId, out SpriteFont? spriteFont))
			{
				return spriteFont;
			}

			return null;
		}


		public void ClearGraphicalAssets()
		{
			foreach (var asset in textureAssets.Values)
			{
				asset.Dispose();
			}

			textureAssets.Clear();
			ttfTextAssets.Clear();
		}
		
	}
}
