using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random.NewFormula
{
    public class Reward : IReward
    {
        public KeyValuePair<int, WedgeWithRange> FindReward(Dictionary<int, WedgeWithRange> finalWedge, decimal trueRandom)
        {
            int key = 0;

            foreach (var item in finalWedge)
            {
                key = item.Key;
                var value = item.Value;
                var inRange = (value.Start <= trueRandom && value.End >= trueRandom);
                if (inRange)
                {
                    Console.WriteLine($"Random Bonus Reward is: {JsonConvert.SerializeObject(item, Formatting.Indented)}");
                    return item;
                    break;
                }
            }

            throw new Exception("Error");
        }
    }
}
