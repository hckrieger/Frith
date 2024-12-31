using Frith.Components;
using Frith.Managers;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TiledLib;

namespace Frith.Extensions
{
	public static class EntityExtensions
	{

	

		public static void LoadSpriteComponents(this Entity entity, string assetName, string filePath, Vector2 position, Game game, Point frameSize = default, int index = 0)
		{

			var textureData = GetTextureData(entity, assetName, filePath, position, game, frameSize);

			var spriteComponent = new SpriteComponent(entity.GetId())
			{
				Rectangle = textureData.GetTextureFrame(index)
			};
			entity.AddComponent(spriteComponent);
		}

		public static void LoadSpriteComponents(this Entity entity, string assetName, string filePath, Vector2 position, Game game, Point frameSize, Point gridPosition)
		{

			var textureData = GetTextureData(entity, assetName, filePath, position, game, frameSize);


			var spriteComponent = new SpriteComponent(entity.GetId())
			{
				Rectangle = textureData.GetTextureFrame(gridPosition)
			};
			entity.AddComponent(spriteComponent);
		}


		public static void LoadBmpTextComponents(this Entity entity, string assetName, string filePath, string text, Color color, Vector2 position, Game game, Point frameSize = default)
		{
		
			var textureData = GetTextureData(entity, assetName, filePath, position, game, frameSize);

			var type = entity.GetType();
			entity.AddComponent(new TextLabelComponent(text, entity.GetId(), color));
		}

		private static TextureData GetTextureData(Entity entity, string assetName, string filePath, Vector2 position, Game game, Point frameSize = default)
		{
			var graphicalAssetManager = game.Services.GetService<GraphicalAssetManager>();

			
			graphicalAssetManager.AddTexture(assetName, filePath);

			entity.AddComponent(new TransformComponent(position));

			var textureData = new TextureData(graphicalAssetManager.GetTexture(assetName), frameSize);

			game.Services.GetService<TextureManager>().AddTextureData(entity.GetId(), textureData);

			return textureData;
		}




		public static void SetSpriteFrame(this Entity entity, int index, Game game)
		{
			ref SpriteComponent spriteComponent = ref ValidateAndGetSpriteComponent(entity, game);

			// Use the common logic to retrieve the texture data and set the frame
			spriteComponent.Rectangle = GetValidatedTextureData(spriteComponent, game).GetTextureFrame(index);
		}

		public static void SetSpriteFrame(this Entity entity, Point gridPosition, Game game)
		{
			ref SpriteComponent spriteComponent = ref ValidateAndGetSpriteComponent(entity, game);

			// Use the common logic to retrieve the texture data and set the frame
			spriteComponent.Rectangle = GetValidatedTextureData(spriteComponent, game).GetTextureFrame(gridPosition);
		}

		// Helper method to validate and retrieve the SpriteComponent
		private static ref SpriteComponent ValidateAndGetSpriteComponent(Entity entity, Game game)
		{
			if (!entity.HasComponent<SpriteComponent>())
			{
				throw new Exception("The entity needs a sprite component to set sprite frame");
			}

			return ref entity.GetComponent<SpriteComponent>();
		}

		// Helper method to validate and retrieve the TextureData
		private static TextureData GetValidatedTextureData(SpriteComponent spriteComponent, Game game)
		{
			TextureManager textureManager = game.Services.GetService<TextureManager>();

			return textureManager.GetTextureData(spriteComponent.TextureId)
				?? throw new Exception($"No texture data found for TextureId {spriteComponent.TextureId}");


		}



		public static bool HasParent(this Entity entity)
		{
			return entity.GetComponent<TransformComponent>().Parent != null;
		}

		public static void SetParent(this Entity entity, Entity parentEntity)
		{
			entity.GetComponent<TransformComponent>().Parent = parentEntity;
		}

		public static Entity? GetParent(this Entity entity)
		{
			return entity.GetComponent<TransformComponent>().Parent;
		}

		public static void RemoveParent(this Entity entity)
		{
			entity.GetComponent<TransformComponent>().Parent = null;
		}
	}
}
