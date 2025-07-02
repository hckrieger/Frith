using Frith.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Frith.Utils;

namespace Frith.Utils
{
	public static class SpriteSheetUtils
	{


		public static Rectangle SourceRectangle(int index, Texture2D texture, Point frameSize)
		{
			if (texture == null)
			{
				throw new Exception("texture can't be null");
			}
			int columns = texture.Width / frameSize.X;

			Point position = GridUtils.IndexToCoordinate(index, columns);

			return new Rectangle(position.X * frameSize.X, position.Y * frameSize.Y, frameSize.X, frameSize.Y);
		}


		public static Rectangle[] GenerationAnimationFrames(int[] frameIndices, Texture2D texture, Point frameSize)
		{
			var frameQuantity = frameIndices.Length;
			Rectangle[] frames = new Rectangle[frameQuantity];	
			for (int i = 0; i < frameQuantity; i++)
			{
				frames[i] = SourceRectangle(frameIndices[i], texture, frameSize);
			}

			return frames;
		}
		


	}
}
