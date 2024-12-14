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
        private int assetId;


		public Rectangle Rectangle { get; set; }

		private int sourceIndex;
        public int SourceIndex => sourceIndex;


        public int AssetId => assetId;

        private float layerDepth;

        public float LayerDepth
        {
            get => layerDepth;
            set => layerDepth = value;
        }

		public SpriteComponent(int assetId = default, float sourceIndex = 0, float layerDepth = .5f)
        {
            Rectangle = Rectangle.Empty;
            this.assetId = assetId;
            this.layerDepth = layerDepth;
        }

  
    }
}
