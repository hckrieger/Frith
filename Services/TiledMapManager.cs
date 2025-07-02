using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class TiledMapManager 
	{
		private TiledMap? currentTiledMap;
		public void SetMainTiledMap(string name)
		{
			currentTiledMap = TiledMapCache?.GetAsset(name);
		}

		public TiledMap? CurrentTiledMap => currentTiledMap;

		public AssetCache<TiledMap?>? TiledMapCache { get; set; }

		public class TiledMap
		{
			[JsonPropertyName("active")]
			public bool Active { get; set; }

			[JsonPropertyName("width")]
			public int Width { get; set; }

			[JsonPropertyName("height")]
			public int Height { get; set; }

			[JsonPropertyName("tilewidth")]
			public int TileWidth { get; set; }

			[JsonPropertyName("tileheight")]
			public int TileHeight { get; set; }

			public int MapPixelWidth => Width * TileWidth;

			public int MapPixelHeight => Height * TileHeight;

			[JsonPropertyName("renderorder")]
			public string? RenderOrder { get; set; }

			[JsonPropertyName("layers")]
			public List<TiledLayer>? Layers { get; set; }

			[JsonPropertyName("tilesets")]
			public List<TiledTileset>? Tilesets { get; set; }
		}

		public class TiledLayer
		{
			[JsonPropertyName("id")]
			public int Id { get; set; }

			[JsonPropertyName("name")]
			public string? Name { get; set; }

			[JsonPropertyName("type")]
			public string? Type { get; set; } // "tilelayer", "objectgroup", etc.

			[JsonPropertyName("opacity")]
			public float Opacity { get; set; }

			[JsonPropertyName("visible")]
			public bool Visible { get; set; }

			[JsonPropertyName("x")]
			public int X { get; set; }

			[JsonPropertyName("y")]
			public int Y { get; set; }

			// Tile layer specific
			[JsonPropertyName("width")]
			public int? Width { get; set; }

			[JsonPropertyName("height")]
			public int? Height { get; set; }

			[JsonPropertyName("data")]
			public List<int>? Data { get; set; }

			// Object layer specific
			[JsonPropertyName("draworder")]
			public string? DrawOrder { get; set; }

			[JsonPropertyName("objects")]
			public List<TiledObject>? Objects { get; set; }

			[JsonPropertyName("properties")]
			public List<CustomProperty?>? CustomProperties { get; set; }
		}

		public class CustomProperty
		{
			[JsonPropertyName("name")]
			public string? Name { get; set; }

			[JsonPropertyName("type")]
			public string? Type { get; set; }

			[JsonPropertyName("value")]
			public object? Value { get; set; }
		}

		public class TiledObject
		{
			[JsonPropertyName("id")]
			public int Id { get; set; }

			[JsonPropertyName("name")]
			public string? Name { get; set; }

			[JsonPropertyName("type")]
			public string? Type { get; set; }

			[JsonPropertyName("x")]
			public float X { get; set; }

			[JsonPropertyName("y")]
			public float Y { get; set; }

			[JsonPropertyName("width")]
			public float Width { get; set; }

			[JsonPropertyName("height")]
			public float Height { get; set; }

			[JsonPropertyName("visible")]
			public bool Visible { get; set; }

			[JsonPropertyName("rotation")]
			public float Rotation { get; set; }

			[JsonPropertyName("point")]
			public bool Point { get; set; }

			[JsonPropertyName("properties")]
			public List<CustomProperty?>? CustomProperties { get; set; }
		}

		public class TiledTileset
		{
			[JsonPropertyName("columns")]
			public int Columns { get; set; }

			[JsonPropertyName("firstgid")]
			public int FirstGid { get; set; }

			[JsonPropertyName("image")]
			public string? Image { get; set; }

			[JsonPropertyName("imageheight")]
			public int ImageHeight { get; set; }

			[JsonPropertyName("imagewidth")]
			public int ImageWidth { get; set; }

			[JsonPropertyName("margin")]
			public int Margin { get; set; }

			[JsonPropertyName("name")]
			public string? Name { get; set; }

			[JsonPropertyName("spacing")]
			public int Spacing { get; set; }

			[JsonPropertyName("tilecount")]
			public int TileCount { get; set; }

			[JsonPropertyName("tileheight")]
			public int TileHeight { get; set; }

			[JsonPropertyName("tilewidth")]
			public int TileWidth { get; set; }
		}
	}
}

