using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public static class Globals<T> where T : class, new()
	{
		private static T? instance;

		public static T Instance()
		{
			if (instance == null)
			{
				instance = new T();
			}

			return instance;
		}
	}
}
