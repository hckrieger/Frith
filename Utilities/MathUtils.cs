using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Frith.Utilities
{
    public static class MathUtils
    {
        private static Random random = new Random();
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int RandomInt(int max)
        {
            return random.Next(max);
        }
    }
}