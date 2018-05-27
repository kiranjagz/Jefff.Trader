using Jefff.Own.Random.Extensions;
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
            var seedWedges = SeedEngine.SeedWedges();
            var seedRewards = SeedEngine.SeedRewardTypes();

            Console.WriteLine($"====================");
            var preSeedReward = Newtonsoft.Json.JsonConvert.SerializeObject(seedRewards, Formatting.Indented);
            Console.WriteLine($"{preSeedReward}");

            var postSeedReward = seedRewards.OrderBy(x => TrueRandomStuff.TrueRandom(0, seedRewards.Where(y => y.Value == 1).Count())).ToList();
            postSeedReward = postSeedReward.OrderBy(x => x.Value).ToList();

            Console.WriteLine($"====================");
            var jefff = Newtonsoft.Json.JsonConvert.SerializeObject(postSeedReward.OrderBy(x => x.Value), Formatting.Indented);
            Console.WriteLine($"{jefff}");

            var maxRewardRange = seedRewards.Count();
            var rewardRange = seedWedges.Count();
            var noRewardRange = maxRewardRange - rewardRange;

            var noRewardPerc = 40.0;
            var reminder = 100.0 - noRewardPerc;

            var newWedgeValues = new Dictionary<int, double>();

            foreach (var item in seedWedges.OrderBy(x => x.Key))
            {
                var wedge = item.Key;
                var newProb = Math.Round(item.Value * reminder / 100, 1, MidpointRounding.AwayFromZero);
                newWedgeValues.Add(item.Key, newProb);
            }

            //Add no reward range onto new wedge
            var noRewardMultiplerPerc = Math.Round(noRewardPerc / noRewardRange, 1, MidpointRounding.AwayFromZero);
            var lastRewardCount = newWedgeValues.OrderByDescending(x => x.Key).FirstOrDefault().Key;

            var index = lastRewardCount;
            for (int i = 0; i < noRewardRange; i++)
            {
                index++;
                newWedgeValues.Add(index, noRewardMultiplerPerc);

            }

            var wedgeWithReward = new Dictionary<int, Wedge>();

            var count = newWedgeValues.Count;

            for (int k = 1; k <= count; k++)
            {
                var value = newWedgeValues[k];
                var wedge = new Wedge { RewardType = postSeedReward[k - 1].Key, Probability = (decimal)value };
                wedgeWithReward.Add(k, wedge);
            }

            Console.WriteLine($"====================");
            var originalWedge = Newtonsoft.Json.JsonConvert.SerializeObject(seedWedges, Formatting.Indented);

            Console.WriteLine($"{originalWedge}");
            var newWedge = Newtonsoft.Json.JsonConvert.SerializeObject(newWedgeValues, Formatting.Indented);

            Console.WriteLine($"====================");
            Console.WriteLine($"{newWedge}");
            var weight = newWedgeValues.Sum(x => x.Value);
            Console.WriteLine($"This is true weight: {weight}");
            Console.WriteLine($"This is rounded weight: {Math.Round(weight)}");

            Console.WriteLine($"====================");
            var realWedge = Newtonsoft.Json.JsonConvert.SerializeObject(wedgeWithReward, Formatting.Indented);
            Console.WriteLine($"{realWedge}");




            var randomEngine = new Dictionary<int, WedgeWithRange>();

            decimal startIndex = 0.00m;

            foreach (var item in wedgeWithReward)
            {
                var chance = item.Value.Probability;
                var start = startIndex;
                var end = startIndex + chance - 1.0m;
                var a = new WedgeWithRange { Start = Math.Round(start,1),  End = Math.Round(end,1), Probability = item.Value.Probability, RewardType = item.Value.RewardType };

                randomEngine.Add(item.Key, a);
                startIndex = end + 0.1m;
            }


            Console.WriteLine($"Random Engine produced: {Newtonsoft.Json.JsonConvert.SerializeObject(randomEngine, Formatting.Indented)}");
            var totalWeight = startIndex - 0.10m;
            Console.WriteLine($"Total Weight: {totalWeight}");
            decimal trueRandom = TrueRandomStuff.TrueRandom(0, (int)totalWeight);
            //trueRandom = 92.61m;
            Console.WriteLine($"Random calculation for engine to compute: {trueRandom}");

            int key = 0;
            foreach (var item in randomEngine)
            {
                key = item.Key;
                var value = item.Value;
                var inRange = (value.Start <= trueRandom && value.End >= trueRandom);
                if (inRange)
                {
                    Console.WriteLine($"Random Bonus Reward is: {JsonConvert.SerializeObject(item, Formatting.Indented)}");
                    break;
                }
            }


            Console.WriteLine($"===================="); ;


            Console.Read();
        }

        private class Wedge
        {
            public string RewardType { get; set; }
            public decimal Probability { get; set; }
        }

        private class WedgeWithRange
        {
            public string RewardType { get; set; }
            public decimal Probability { get; set; }
            public decimal Start { get; set; }
            public decimal End { get; set; }
        }
    }
}
