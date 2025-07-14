using Frith.ECS.Components;
using Frith.Services;
using Frith.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Systems
{
	public class SpriteRenderSystem : EcsSystem, IRenderable
	{
		SpriteBatch spriteBatch;
		AssetCache<Texture2D> textureCache;
		public SpriteRenderSystem(Game game, SpriteBatch spriteBatch)
		{
			RequireComponent<TransformComponent>();
			RequireComponent<SpriteComponent>();
			this.spriteBatch = spriteBatch;	
			textureCache = game.Services.GetService<AssetCache<Texture2D>>();

		}



		public override void Draw()
		{
			foreach (var entity in GetSystemEntities())
			{
				var transform = entity.GetComponent<TransformComponent>();
				ref var sprite = ref entity.GetComponent<SpriteComponent>();

				Rectangle destinationRectangle = new Rectangle
				(
					(int)Math.Round(transform.Position.X),
					(int)Math.Round(transform.Position.Y),
					sprite.Width * (int)transform.Scale.X,
					sprite.Height * (int)transform.Scale.Y
				);

				Texture2D? texture = textureCache.GetAsset(sprite.Name);

				if (entity.HasComponent<AnimationComponent>())
				{
					var animationComponent = entity.GetComponent<AnimationComponent>();

					sprite.SourceRegion = animationComponent.CurrentFrame;
				} else
				{
					if (sprite.Index == 0)
					{
						sprite.SourceRegion = new Rectangle(0, 0, sprite.Width, sprite.Height);
					}
					else
					{
						sprite.SourceRegion = SpriteSheetUtils.SourceRectangle(sprite.Index, texture, new Point(sprite.Width, sprite.Height));
					}
				}



				spriteBatch.Draw(texture, destinationRectangle, sprite.SourceRegion, sprite.Color, transform.Rotation, transform.Origin, SpriteEffects.None, .5f);
			}
		}
	}
}
