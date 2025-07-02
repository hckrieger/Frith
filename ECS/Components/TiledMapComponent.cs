using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.ECS.Components
{
	public struct TiledMapComponent(string name)
	{
		public string Name { get; set; } = name;
	}
}
