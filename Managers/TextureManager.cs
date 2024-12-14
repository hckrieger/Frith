﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Managers
{
	public class TextureManager
	{
		private Dictionary<int, TextureData?> textureDataDictionary = new Dictionary<int, TextureData?>();	

		public Dictionary<int, TextureData?> GetAllTextureData()
		{
			return textureDataDictionary;
		}
	
		public void AddTextureData(int textureDataKey, TextureData textureDataValue)
		{
			if (textureDataDictionary.TryAdd(textureDataKey, textureDataValue))
			{
				textureDataValue.AssetId = textureDataKey;
			}
		}

		public TextureData? GetTextureData(int textureDataKey)
		{
			if (textureDataDictionary.TryGetValue(textureDataKey, out TextureData? textureDataValue))
			{
				return textureDataValue;
			}

			return null;
		}

		public void RemoveTextureData(int textureDataKey)
		{
			if (textureDataDictionary.ContainsKey(textureDataKey))
			{
				textureDataDictionary.Remove(textureDataKey);
			} else
			{
				Logger.Error($"The texture data value of {textureDataKey} doesn't exists");
			}
		}

		

	}
}