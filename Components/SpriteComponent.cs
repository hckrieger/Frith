using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Components
{
	public struct SpriteComponent
	{
        public Rectangle Rectangle { get; set; }



        public int TextureId { get; }

        public float LayerDepth { get; set; }

        public SpriteComponent(int textureId = 0, float sourceIndex = 0, float layerDepth = .5f)
        {
            Rectangle = Rectangle.Empty;
            this.TextureId = textureId;
            this.LayerDepth = layerDepth;
        }

  
    }
}
