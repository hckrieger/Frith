using Frith.ECS.Components;
using Frith.Services;
using Frith.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Frith.Services.TiledMapManager;

namespace Frith.ECS.Systems
{
	public class TilemapRenderSystem : EcsSystem, IRenderable
	{

		private AssetCache<TiledMap?> tilemapCache;
		private AssetCache<Texture2D> textureCache;
		private SpriteBatch spriteBatch;
		private TiledMapManager tiledMapManager;
		public TilemapRenderSystem(Game game, SpriteBatch spriteBatch)
		{
			RequireComponent<TransformComponent>();
			RequireComponent<TiledMapComponent>();

			tilemapCache = game.Services.GetService<AssetCache<TiledMap?>>();
			textureCache = game.Services.GetService<AssetCache<Texture2D>>();
			tiledMapManager = game.Services.GetService<TiledMapManager>();

			this.spriteBatch = spriteBatch;
		}

		private TiledTileset FindTilesetForGid(TiledMap tiledMap, int gid)
		{
			if (tiledMap.Tilesets != null)
			{
				for (int i = tiledMap.Tilesets.Count - 1; i >= 0; i--)
				{
					if (gid >= tiledMap.Tilesets[i].FirstGid)
					{
						return tiledMap.Tilesets[i];
					}
				}
			}


			throw new Exception($"No matching tileset found for {gid}");
		}



		private Rectangle SourceRectangle(int gid, TiledTileset tileset)
		{
			int localId = gid - tileset.FirstGid;

			Point textureCoordinate = GridUtils.IndexToCoordinate(localId, tileset.Columns);

			Rectangle sourceRectangle = new Rectangle(textureCoordinate.X * tileset.TileWidth, textureCoordinate.Y * tileset.TileHeight, tileset.TileWidth, tileset.TileHeight);

			return sourceRectangle;
		}

		private Vector2 TilePosition(int index, TiledMap tiledMap, TiledLayer layer)
		{
			if (layer.Width != null && tiledMap != null && layer.Type == "tilelayer" && layer.Data != null)
			{
				Point positionCoordinate = GridUtils.IndexToCoordinate(index, layer.Width.Value);

				
				return new Vector2(positionCoordinate.X * tiledMap.TileWidth, positionCoordinate.Y * tiledMap.TileHeight);
			}

			throw new Exception("layer type needs to be tilelayer");

		}

		public override void Draw()
		{
			base.Draw();

			

				TiledMap? tilemap = tiledMapManager.CurrentTiledMap;

				if (tilemap == null || tilemap.Layers == null)
					return;

				foreach (var layer in tilemap.Layers)
				{
					if (layer.Type == "tilelayer")
					{

						for (int i = 0; i < layer?.Data?.Count; i++)
						{
							var gid = layer.Data[i];
							if (gid == 0)
								continue;

							var tileset = FindTilesetForGid(tilemap, gid);
							var texture = textureCache.GetAsset(tileset.Name ?? throw new Exception("Tiled Tileset name can't be null"));

							spriteBatch.Draw(texture, TilePosition(i, tilemap, layer), SourceRectangle(gid, tileset), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, .05f);
						}

					}
					else if (layer.Type == "objectgroup")
					{
						continue;
					}
				}
			
		}
	}
}
