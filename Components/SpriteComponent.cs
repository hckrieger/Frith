﻿using Microsoft.Xna.Framework;
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
        private int textureId;


		public Rectangle Rectangle { get; set; }



        public int TextureId => textureId;

        private float layerDepth;

        public float LayerDepth
        {
            get => layerDepth;
            set => layerDepth = value;
        }

		public SpriteComponent(int textureId = default, float sourceIndex = 0, float layerDepth = .5f)
        {
            Rectangle = Rectangle.Empty;
            this.textureId = textureId;
            this.layerDepth = layerDepth;
        }

  
    }
}
