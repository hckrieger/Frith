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

		private readonly Dictionary<string, Texture2D> textureAssets = new();
	
		public void AddTexture(string assetId, string filePath)
		{
			if (textureAssets.ContainsKey(assetId)) return;

			try
			{
				var texture = content.Load<Texture2D>(filePath);

				textureAssets[assetId] = texture;
			} catch (ContentLoadException e)
			{
				Logger.Error($"Failed to load texture {filePath}. Exception: {e.Message}");
			}

		}

		public Texture2D? GetTexture(string assetId)
		{
			return textureAssets.GetValueOrDefault(assetId);
		}





		public void ClearGraphicalAssets()
		{
			foreach (var asset in textureAssets.Values)
			{
				asset.Dispose();
			}

			textureAssets.Clear();
		}
		
	}
}
