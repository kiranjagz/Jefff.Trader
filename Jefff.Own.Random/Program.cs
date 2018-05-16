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

            for (int i = 0; i < 3; i++)
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
                    startIndex = end++;
                }

                Console.WriteLine($"Random Engine produced: {Newtonsoft.Json.JsonConvert.SerializeObject(randomEngine, Formatting.Indented)}");
                var endIndex = startIndex;
                Console.WriteLine($"Last index range: {endIndex}");
                System.Random random = new System.Random(DateTime.Now.Millisecond);
                var generateARandomNumber = random.Next(1, endIndex);
                Console.WriteLine($"Random calculation for engine to compute: {generateARandomNumber}");

                string key = string.Empty;
                foreach (var item in randomEngine)
                {
                    key = item.Key;
                    var value = item.Value;
                    var inRange = (value.Start <= generateARandomNumber && value.End >= generateARandomNumber);
                    if (inRange)
                        break;
                }

                Console.WriteLine($"Random Bonus Reward is: {key}");
                Console.WriteLine($"====================");
            }
            Console.Read();
        }

        private int TrueRandom()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                int randomvalue = BitConverter.ToInt32(rno, 0);
                return randomvalue;
            }
        }

        private class RangeSelector
        {
            public int Start { get; set; }
            public int End { get; set; }
        }
    }
}
