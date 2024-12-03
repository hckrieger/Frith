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
		private Vector2 position;

		public Vector2 Position
		{
			get => position;
			set => position = value;
		}

		private string text;

		public string Text
		{
			get => text;
			set => text = value;
		}

		private string assetId;

		private Color color;

		public Color Color
		{
			get => color;
			set => color = value;
		}

		private bool isFixed;

		public TextLabelComponent(Vector2 position = default, string text = "", string assetId = "", Color color = default, bool isFixed = true)
		{
			this.position = position;
			this.text = text;
			this.assetId = assetId;
			this.color = color;
			this.isFixed = isFixed;
		}
	}
}
