using Jefff.Own.Random.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random.JagFormula
{
    public class JagFormula
    {
        public void DoSomethingFancy(Dictionary<string, int> seededData)
        {        
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine($"====================");

                var randomEngine = new Dictionary<string, RangeSelector>();
                System.Random r = new System.Random(DateTime.Now.Second);
                seededData = seededData.OrderBy(x => TrueRandomStuff.TrueRandom(0, seededData.Count)).ToDictionary(item => item.Key, item => item.Value);
                Console.WriteLine($"Seeed data: {Newtonsoft.Json.JsonConvert.SerializeObject(seededData, Formatting.Indented)}");

                int startIndex = 0;

                foreach (var item in seededData)
                {
                    var chance = item.Value;
                    var start = startIndex;
                    var end = startIndex + chance - 1;
                    var a = new RangeSelector { Start = start, End = end };

                    randomEngine.Add(item.Key, a);
                    startIndex = end + 1;
                }

                Console.WriteLine($"Random Engine produced: {Newtonsoft.Json.JsonConvert.SerializeObject(randomEngine, Formatting.Indented)}");
                var totalWeight = startIndex;
                Console.WriteLine($"Total Weight: {totalWeight}");
                System.Random random = new System.Random(DateTime.Now.Millisecond);
                var generateARandomNumber = random.Next(0, totalWeight - 1);
                var trueRandom = TrueRandomStuff.TrueRandom(0, totalWeight - 1);
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
        }

        private class RangeSelector
        {
            public int Start { get; set; }
            public int End { get; set; }
        }
    }
}
