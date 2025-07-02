using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Utils
{
	public static class GridUtils
	{
		public static int CoordinateToIndex(Point coordinate, int width)
		{
			return coordinate.Y * width + coordinate.X;
		}

		public static Point IndexToCoordinate(int index, int width)
		{
			int x = index % width;
			int y = index / width;
			return new Point(x, y);
		}
	}
}
