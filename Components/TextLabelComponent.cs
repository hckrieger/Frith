using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct TextLabelComponent
	{

		private string text;

		public string Text
		{
			get => text;
			set => text = value;
		}

		private int assetId;
		public int AssetId
		{
			get => assetId;
			set => assetId = value;
		}

		private Color color;

		public Color Color
		{
			get => color;
			set => color = value;
		}


		public TextLabelComponent(string text = "", int assetId = default, Color color = default)
		{
			this.text = text;
			this.assetId = assetId;
			this.color = color;
		}
	}
}
