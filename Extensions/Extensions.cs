using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith.Extensions
{
	public static class BitArrayExtensions
	{
		public static bool AreValuesEqual(this BitArray a, BitArray b)
		{
			if (a.Length != b.Length) return false;

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}

			return true;
		}
	}
}
