using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class AssetCache<TAsset>
	{
		private Func<string, TAsset> contentProcess;

		private Dictionary<string, TAsset?> assets = new Dictionary<string, TAsset?>();

		private static string AssetTypeName => typeof(TAsset).Name;

		public List<string> AssetKeys { get; set; } = new List<string>();

		public AssetCache(Func<string, TAsset> contentProcess)
		{
			
			this.contentProcess = contentProcess;
			
		}
		public void AddAsset(string name, string filePath)
		{
			if (!assets.TryGetValue(name, out _))
			{
				
				assets.Add(name, contentProcess(filePath));
				Logger.Info($"New {AssetTypeName} added to asset store with id '{name}'");
			}
			else
			{
				Logger.Warn($"Tried to add {AssetTypeName} that was already added");
			}
			ResetAssetKeys();
		}

		public void RemoveAsset(string name)
		{
			if (assets.ContainsKey(name))
			{
				if (assets[name] is IDisposable disposableAsset)
				{
					disposableAsset.Dispose();
				}
				
				assets?.Remove(name);
			}

			ResetAssetKeys();
		}

		public void ClearAssets()
		{

		
			foreach (var asset in assets.Values)
			{
				if (asset is IDisposable disposableAsset)
				{
					disposableAsset?.Dispose();
				}
			}
			

			assets.Clear();
			ResetAssetKeys();
		}

		public TAsset GetAsset(string name)
		{
			if (assets.TryGetValue(name, out TAsset? asset) && asset != null)
			{
				return asset;
			}

			throw new Exception($"{AssetTypeName} with asset id {name} doesn't exist in asset store");
		}

		public bool HasAsset(string name)
		{
			return assets.ContainsKey(name);
		}

		public void ResetAssetKeys()
		{
			AssetKeys.Clear();
			AssetKeys = assets.Keys.ToList();
		}
	}
}
