using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct SpriteComponent
	{

		public int Index { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Color Color { get; set; }

		public Rectangle SourceRegion { get; set; }

		public string Name { get; private set; }

		public SpriteComponent(string name, Point size, int index = 0)
		{
			Name = name;	
			Width = size.X;
			Height = size.Y;
			Color = Color.White;
			Index = index;
		}

	}
}
