using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct TextLabelComponent
	{

		private string text;

		private char[] textCharacters;

		public char[] TextCharacters => textCharacters;

		public string Text
		{
			get => text;
			set
			{
				if (value == text)
					return;

				text = value;
				
				if (textCharacters == null || textCharacters.Length != text.Length)
				{
					textCharacters = new char[text.Length];
				}

				text.CopyTo(0, textCharacters, 0, text.Length);
			}
		}

		private int textureId;
		public int TextureId
		{
			get => textureId;
			set => textureId = value;
		}

		private Color color;

		public Color Color
		{
			get => color;
			set => color = value;
		}


		public TextLabelComponent(string text = "", int textureId = default, Color color = default)
		{
			textCharacters = text.ToCharArray();
			this.text = text;
			this.textureId = textureId;
			this.color = color;
		}

	}
}
