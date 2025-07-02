using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public static class Extensions
	{
		
		public static bool BitsetsMatch(this BitArray bitArray1, BitArray bitArray2)
		{
			for (int i = 0; i < bitArray1.Length; i++)
			{
				
					if (bitArray1[i] != bitArray2[i])
						return false;
					
				
			}

			return true;
		}



	}
}
