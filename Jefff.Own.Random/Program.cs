using Jefff.Own.Random.Seed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random
{
    class Program
    {
        static void Main(string[] args)
        {
            var seededData = SeedEngine.SeedData();

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"====================");

                var randomEngine = new Dictionary<string, RangeSelector>();
                System.Random r = new System.Random(DateTime.Now.Second);
                seededData = seededData.OrderBy(x => r.Next(0, seededData.Count)).ToDictionary(item => item.Key, item => item.Value);
                Console.WriteLine($"Seeed data: {Newtonsoft.Json.JsonConvert.SerializeObject(seededData, Formatting.Indented)}");

                int startIndex = 1;

                foreach (var item in seededData)
                {
                    var chance = item.Value;
                    var start = startIndex;
                    var end = startIndex + chance;
                    var a = new RangeSelector { Start = start, End = end };

                    randomEngine.Add(item.Key, a);
                    startIndex = end + 1;
                }

                Console.WriteLine($"Random Engine produced: {Newtonsoft.Json.JsonConvert.SerializeObject(randomEngine, Formatting.Indented)}");
                var totalWeight = startIndex;
                Console.WriteLine($"Total Weight: {totalWeight}");
                System.Random random = new System.Random(DateTime.Now.Millisecond);
                var generateARandomNumber = random.Next(1, totalWeight);
                var trueRandom = TrueRandom(1, totalWeight);
                Console.WriteLine($"Random calculation for engine to compute: {trueRandom}");

                string key = string.Empty;
                foreach (var item in randomEngine)
                {
                    key = item.Key;
                    var value = item.Value;
                    var inRange = (value.Start <= trueRandom && value.End >= trueRandom);
                    if (inRange)
                        break;
                }

                Console.WriteLine($"Random Bonus Reward is: {key}");
                Console.WriteLine($"====================");
            }
            Console.Read();
        }

        private static int TrueRandom(int minValue, int maxValue)
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

        private class RangeSelector
        {
            public int Start { get; set; }
            public int End { get; set; }
        }
    }
}
