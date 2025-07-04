using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct BmpTextComponent(string text, string textureName, Point frameSize, Color color)
	{
		private string text = text;

		public char[] TextCharacters { get; private set; } = text.ToCharArray();

		public string Text
		{
			get => text;
			set
			{
				if (value == text)
					return;

				text = value;

				if (TextCharacters == null || TextCharacters.Length != text.Length)
				{
					TextCharacters = new char[text.Length];	
				}

				text.CopyTo(0, TextCharacters, 0, text.Length);
			}
		}

		public string TextureName { get; set; } = textureName;

		public Color Color { get; set; } = color;

		public Point FrameSize { get; set; } = frameSize;
	}
	
}
