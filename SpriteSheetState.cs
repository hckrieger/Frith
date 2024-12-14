using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public class SpriteSheetState
	{

		private Texture2D texture;
		private Point frameSize;

		public int Columns => texture.Width / frameSize.X;
		public int Rows => texture.Height / frameSize.Y;

		public SpriteSheetState(Texture2D texture, Point frameSize)
		{
			this.texture = texture;
			this.frameSize = frameSize;
		}

		public Rectangle GetSpriteFrameRectangle(int frameIndex = 0)
		{

			int rowIndex = frameIndex / Columns;
			int columnIndex = frameIndex % Columns;

			return new Rectangle(columnIndex * frameSize.X, rowIndex * frameSize.Y, texture.Width, texture.Height);
		}
	}
}
