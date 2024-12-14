using Frith.Components;
using Frith.Managers;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Extensions
{
	public static class EntityExtensions
	{

		

		public static void LoadSpriteComponents(this Entity entity, string assetName, string filePath, Vector2 position, Game game, Point frameSize = default, int index = 0)
		{

			TextureData textureData;

			CommonComponents(entity, assetName, filePath, frameSize, out textureData, position, game);

			SpriteComponent spriteComponent = new SpriteComponent(entity.GetId());
			spriteComponent.Rectangle = textureData.GetTextureFrame(index);
			entity.AddComponent(spriteComponent);
		}


		public static void LoadBmpTextComponents(this Entity entity, string assetName, string filePath, string text, Color color, Vector2 position, Game game, Point frameSize = default)
		{

			TextureData textureData;


			CommonComponents(entity, assetName, filePath, frameSize, out textureData, position, game);


			entity.AddComponent(new TextLabelComponent(text, entity.GetId(), color));
		}


		public static void CommonComponents(Entity entity, string assetName, string filePath, Point frameSize, out TextureData textureData, Vector2 position, Game game)
		{
			GraphicalAssetManager graphicalAssetManager = game.Services.GetService<GraphicalAssetManager>();

			graphicalAssetManager.AddTexture(assetName, filePath);
			textureData = new TextureData(graphicalAssetManager.GetTexture(assetName), frameSize);
			game.Services.GetService<TextureManager>().AddTextureData(entity.GetId(), textureData);

			entity.AddComponent(new TransformComponent(position));
		}

	}
}
