using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct TextLabelComponent(string text = "", int textureId = 0, Color color = default)
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

		public int TextureId { get; set; } = textureId;

		public Color Color { get; set; } = color;
	}
}
