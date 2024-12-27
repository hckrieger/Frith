using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frith
{
	public class TextureData
	{
		public int TextureId { get; set; }	
		public Texture2D Texture { get; set; }

		public int Width => Texture.Width;

		public int Height => Texture.Height;

		public int Columns => Width / FrameSize.X;

		public int Rows => Height / FrameSize.Y;

		public Point FrameSize { get; set; }

		public TextureData(Texture2D texture, Point frameSize = default)
		{
 
			TextureId = 0;
			Texture = texture;

			if (frameSize == default)
				frameSize = new Point(Width, Height);

			FrameSize = frameSize;

	
		}

		public Rectangle GetTextureFrame(int index)
		{
			var rowIndex = index / Columns;
			var columnIndex = index % Columns;

			if (rowIndex < 0 || rowIndex >= Rows || columnIndex < 0 || columnIndex >= Columns)
			{
				throw new IndexOutOfRangeException($"The index {index} is out of range of the number of texture frames");
			}

			return new Rectangle(columnIndex * FrameSize.X, rowIndex * FrameSize.Y, FrameSize.X, FrameSize.Y);

		}

		public Rectangle GetTextureFrame(Point point)
		{
			if (point.Y < 0 || point.Y >= Rows || point.X < 0 || point.X >= Columns)
			{
				throw new IndexOutOfRangeException($"The sprite sheet position ({point.X}, {point.Y}) is out of range of the number of texture frames");
			}

			return new Rectangle(point.X * FrameSize.X, point.Y * FrameSize.Y, FrameSize.X, FrameSize.Y);
		}
	}
}
