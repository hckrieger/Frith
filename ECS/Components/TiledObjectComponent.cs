using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct TiledObjectComponent(string name, bool ranOnce = false, bool isResetable = true)
	{
		public string Name { get; set; } = name;

		public bool RanOnce { get; set; } = ranOnce;
		public bool IsResetable { get; private set; } = isResetable;
	}
}
