using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random.Extensions
{
    public static class TrueRandomStuff
    {
        public static int TrueRandom(int minValue, int maxValue)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                var _uint32Buffer = new byte[4];
                if (minValue > maxValue) throw new ArgumentOutOfRangeException("The min value is larger than the max!");
                if (minValue == maxValue) return minValue;
                var diff = maxValue - minValue;

                while (true)
                {
                    provider.GetBytes(_uint32Buffer);
                    var random = BitConverter.ToUInt32(_uint32Buffer, 0);
                    var max = (1 + (Int64)UInt32.MaxValue);
                    var remainder = max % diff;

                    if (random < max - remainder)
                    {
                        return (Int32)(minValue + (random % diff));
                    }
                }
            }
        }
    }
}
