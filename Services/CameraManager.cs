using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Services
{
	public class CameraManager
	{
		public float Zoom { get; set; } = 1f;

		private Vector2 location;
		public Vector2 Location
		{
			get => location;
			set
			{
				var x = (int)Math.Round(value.X);
				var y = (int)Math.Round(value.Y);	
				location = new Vector2(x, y);
			}
		}

		public float Rotation { get; set; } = 0.0f;


		private Rectangle bounds;

		public Rectangle VisibleCameraArea
		{
			get
			{
				return bounds;
			}

			set
			{
				bounds = value;
			}
		}

		public Matrix TransformMatrix
		{
			get
			{
				return
					Matrix.CreateTranslation(-Location.X, -Location.Y, 0) *
					Matrix.CreateRotationZ(Rotation) *
					Matrix.CreateScale(Zoom, Zoom, 1f) *
					Matrix.CreateTranslation(new Vector3(bounds.Width * .5f, bounds.Height * .5f, 0));
			}
		}




		public Rectangle VisibleWorldArea
		{

			get
			{
				var inverseViewMatrix = Matrix.Invert(TransformMatrix);
				var topLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
				var topRight = Vector2.Transform(new Vector2(bounds.Width, 0), inverseViewMatrix);
				var bottomLeft = Vector2.Transform(new Vector2(0, bounds.Height), inverseViewMatrix);
				var bottomRight = Vector2.Transform(new Vector2(bounds.Width, bounds.Height), inverseViewMatrix);

				var min = new Vector2(
					MathHelper.Min(topLeft.X, MathHelper.Min(topRight.X, MathHelper.Min(bottomLeft.X, bottomRight.X))),
					MathHelper.Min(topLeft.Y, MathHelper.Min(topRight.Y, MathHelper.Min(bottomLeft.Y, bottomRight.Y)))
				);

				var max = new Vector2(
					MathHelper.Max(topLeft.X, MathHelper.Max(topRight.X, MathHelper.Max(bottomLeft.X, bottomRight.X))),
					MathHelper.Max(topLeft.Y, MathHelper.Max(topRight.Y, MathHelper.Max(bottomLeft.Y, bottomRight.Y)))
				);


				return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
			}

		}

		public Vector2 ScreenToWorld(Vector2 position)
		{
			return Vector2.Transform(position, Matrix.Invert(TransformMatrix));
		}

		public Vector2 WorldToScreen(Vector2 position)
		{
			return Vector2.Transform(position, TransformMatrix);
		}

		public void CameraLookAt(Vector2 playerPosition, Vector2 offset = default)
		{

			Location = playerPosition - offset;
		}
	}
}