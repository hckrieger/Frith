using Frith.ECS.Components;
using Frith.Services;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Frith.Services.TiledMapManager;

namespace Frith.ECS.Systems
{
	public class TiledObjectSystem : EcsSystem
	{
		private AssetCache<TiledMap> tiledMapCache;
		public TiledObjectSystem(Game game)
		{
			RequireComponent<TiledObjectComponent>();
			RequireComponent<TransformComponent>();
			tiledMapCache = game.Services.GetService<AssetCache<TiledMap>>();

			//SetSpawnObjects();
		}

		public override void Update(GameTime gameTime)
		{
			SetSpawnObjects();
		}


		public void SetSpawnObjects()
		{
			foreach (string key in tiledMapCache.AssetKeys)
			{
				TiledMap? tiledMap = tiledMapCache.GetAsset(key);


				if (tiledMap == null || tiledMap?.Layers == null)
					return;

				foreach (var layer in tiledMap.Layers)
				{
					if (layer.Type == "objectgroup" && layer.Objects != null)
					{
						foreach (var obj in layer.Objects)
						{
							foreach (var entity in GetSystemEntities())
							{
								if (entity.GetComponent<TiledObjectComponent>().RanOnce == true)
									return;

								if (entity.GetComponent<TiledObjectComponent>().Name == obj.Name)
								{

									float rotation = 0.0f;
									Vector2 scale = Vector2.One;

									if (obj.CustomProperties != null)
									{
										foreach (var property in obj.CustomProperties)
										{
											if (property?.Name == "rotation" && property.Value != null)
											{
												rotation = (float)property.Value;
											}

											if (property?.Name == "scale" && property.Value != null)
											{
												scale = (Vector2)property.Value;
											}
										}
									}



									ref var transformComponent = ref entity.GetComponent<TransformComponent>();
									transformComponent.Scale = scale;
									transformComponent.Position = new Vector2(obj.X, obj.Y);
									transformComponent.Rotation = rotation;

									ref var tiledObjectComponent = ref entity.GetComponent<TiledObjectComponent>();
									tiledObjectComponent.RanOnce = true;

								}
							}
						}
					}
					else
					{
						continue;
					}
				}




			}
		}
	}


}
