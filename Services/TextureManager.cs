using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class TextureManager(ContentManager content, SpriteBatch spriteBatch)
	{
		public SpriteBatch SpriteBatch { get; set; } = spriteBatch;

		private Dictionary<string, Texture2D?> textures = new Dictionary<string, Texture2D?>();

		public void AddTexture(string name, string filePath)
		{
			if (!textures.TryGetValue(name, out Texture2D? texture))
			{
				texture = content.Load<Texture2D>(filePath);
				textures.Add(name, texture);
				Logger.Info($"New texture added to asset store with id {name}");
			} else
			{
				Logger.Warn($"Tried to add texture that  already added");
			}
		}

		public void RemoveTexture(string name)
		{
			if (textures.ContainsKey(name))
			{
				textures?[name]?.Dispose();
				textures?.Remove(name);
			}

			
		}

		public void ClearTextures()
		{
			foreach (var texture in textures.Values)
			{
				texture?.Dispose();
			}
			textures.Clear();
		}

		public Texture2D GetTexture(string name)
		{
			if (textures.TryGetValue(name, out Texture2D? texture) && texture != null)
			{
				return texture;
			}

			throw new Exception($"Texutre with asset id {name} doesn't exist in asset store");
		}
	}
}
