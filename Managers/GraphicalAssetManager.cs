using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class GraphicalAssetManager(ContentManager content)
	{

		private readonly Dictionary<string, Texture2D> textureAssets = new Dictionary<string, Texture2D>();
		private readonly Dictionary<string, SpriteFont> ttfTextAssets = new Dictionary<string, SpriteFont>();

		public void AddTexture(string assetId, string filePath)
		{
			if (textureAssets.ContainsKey(assetId)) return;
			
			var texture = content.Load<Texture2D>(filePath);

			textureAssets[assetId] = texture;
		}

		public Texture2D? GetTexture(string assetId)
		{
			return textureAssets.GetValueOrDefault(assetId);
		}


		public void AddSpriteFontTtf(string assetId, string filePath)
		{
			if (textureAssets.ContainsKey(assetId)) return;
			var texture = content.Load<SpriteFont>(filePath);

			ttfTextAssets[assetId] = texture;
		}

		public SpriteFont? GetSpriteFontTtf(string assetId)
		{
			return ttfTextAssets.GetValueOrDefault(assetId);
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
