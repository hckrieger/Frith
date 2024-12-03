using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public class AssetStore
	{
        private Dictionary<string, Texture2D?>? textures = new Dictionary<string, Texture2D?>();
        private Dictionary<string, SpriteFont?>? fonts = new Dictionary<string, SpriteFont?>();
        private ContentManager? content;

        public AssetStore(ContentManager content)
        {
            Logger.Info("AssetStore constructor called!");
            this.content = content;
        }

        ~AssetStore()
        {
            ClearAssets();
            Logger.Info("Asset Store destructor called!");

        }

        public void ClearAssets()
        {
            if (textures == null)
                return; 

            foreach (KeyValuePair<string, Texture2D?> kvp in textures)
            {
                content?.Unload();
                kvp.Value?.Dispose();
            }

            //foreach (KeyValuePair<>)
        }

        public void AddTexture(string assetId, string filePath)
        {
            Texture2D? texture = content?.Load<Texture2D>(filePath);

			textures?.Add(assetId, texture);

            Logger.Info($"New texture added to the Asset Store with id {assetId}");
        }

		public Texture2D? GetTexture(string assetId)
		{
			if (textures == null)
				return null;

			if (textures.TryGetValue(assetId, out Texture2D? texture))
			{
				return texture;
			}

			return null;
		}

		public void AddFont(string assetId, string filePath)
        {
            SpriteFont? font = content?.Load<SpriteFont>(filePath);

            fonts?.Add(assetId, font);

            Logger.Info($"New font added to the Asset Store with id {assetId}");
        }

        public SpriteFont? GetFont(string assetId)
        {
            if (fonts == null)
                return null;

            if (fonts.TryGetValue(assetId,out SpriteFont? font))
            {
                return font;
            }

            return null;
        }
    }
}
