using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct SpriteComponent
	{
        public Rectangle Rectangle { get; set; }


        public Color Color { get; set; }

        public int TextureId { get; }

        public float LayerDepth { get; set; }

        public bool Visible { get; set; } = true;

        public SpriteComponent(int textureId = 0, float sourceIndex = 0, float layerDepth = .5f, Color color = default)
        {
            Rectangle = Rectangle.Empty;
            this.TextureId = textureId;
            this.LayerDepth = layerDepth;

            if (color.Equals(default))
            {
                Color = Color.White;
            }
        }

  
    }
}
