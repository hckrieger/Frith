using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public class TextureData
	{
		public int AssetId { get; set; }	
		public Texture2D Texture { get; set; }

		public int Width => Texture.Width;

		public int Height => Texture.Height;

		public int Columns => Width / FrameSize.X;

		public int Rows => Height / FrameSize.Y;

		public Point FrameSize { get; set; }

		public TextureData(Texture2D texture, Point frameSize = default)
		{
 
			AssetId = default;
			Texture = texture;

			if (frameSize == default)
				frameSize = new Point(Width, Height);

			FrameSize = frameSize;

	
		}

		public Rectangle GetTextureFrame(int index)
		{
			int rowIndex = index / Columns;
			int columnIndex = index % Columns;

			return new Rectangle(columnIndex * FrameSize.X, rowIndex * FrameSize.Y, FrameSize.X, FrameSize.Y);

		}
	}
}
