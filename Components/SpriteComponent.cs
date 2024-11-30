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
        private string assetId;

		private int width;
        public int Width
        {
            get => width;
            set => width = value;
        }

		private int height;
        public int Height
        {
            get => height;
            set => height = value;
        }

        private int srcRectX;

        public int SrcRectX
        {
            set
            {
                srcRectX = value;
                sourceRectangle.X = srcRectX;

            }
        }

        private int srcRectY;
        public int SrcRectY
        {
            set
            {
                srcRectY = value;
                sourceRectangle.Y = srcRectY;
            }
        }

        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get => sourceRectangle;
            set => sourceRectangle = value;
        }

        public string AssetId => assetId;

        private float layerDepth;

        public float LayerDepth
        {
            get => layerDepth;
            set => layerDepth = value;
        }

        public SpriteComponent(string assetId = "", int width = 0, int height = 0, float layerDepth = .5f, int srcRectX = 0, int srcRectY = 0)
        {
            this.assetId = assetId;
            this.width = width;
            this.height = height;
            this.srcRectX = srcRectX;
            this.layerDepth = layerDepth;
            this.sourceRectangle = new Rectangle(srcRectX, srcRectY, width, height);
        }

  
    }
}
