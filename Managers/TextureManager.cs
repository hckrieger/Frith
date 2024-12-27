using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class TextureManager
	{
		private readonly Dictionary<int, TextureData?> textureDataDictionary = new Dictionary<int, TextureData?>();	

		public Dictionary<int, TextureData?> GetAllTextureData()
		{
			return textureDataDictionary;
		}
	
		public void AddTextureData(int textureDataKey, TextureData textureDataValue)
		{
			if (textureDataDictionary.TryAdd(textureDataKey, textureDataValue))
			{
				textureDataValue.TextureId = textureDataKey;
			}
		}

		public TextureData? GetTextureData(int textureDataKey)
		{
			return textureDataDictionary.GetValueOrDefault(textureDataKey);
		}

		public void RemoveTextureData(int textureDataKey)
		{
            if (!textureDataDictionary.Remove(textureDataKey))
            {
                Logger.Error($"The texture data value of {textureDataKey} doesn't exists");
            }
        }

		

	}
}
