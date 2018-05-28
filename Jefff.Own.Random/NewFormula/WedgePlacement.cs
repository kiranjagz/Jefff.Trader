using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Own.Random.Seed;
using static Jefff.Own.Random.Program;

namespace Jefff.Own.Random.NewFormula
{
    public class WedgePlacement : IWedgePlacement
    {
        public Dictionary<int, Wedge> MapWedgeRewards(Dictionary<int, SeedEngine.Pot> newWedgeIncludingNoRewards,
            List<KeyValuePair<string, int>> randomOrderedRewards)
        {
            var wedgeAssignedToReward = new Dictionary<int, Wedge>();
            var count = newWedgeIncludingNoRewards.Count;
            for (int k = 1; k <= count; k++)
            {
                var value = newWedgeIncludingNoRewards[k];
                var wedge = new Wedge
                {
                    RewardType = randomOrderedRewards[k - 1].Key,
                    Probability = value.Probability,
                    Slope = value.Slope
                };
                wedgeAssignedToReward.Add(k, wedge);
            }

            return wedgeAssignedToReward;
        }

        public Dictionary<int, WedgeWithRange> FinalWedgeWithRange(Dictionary<int, Wedge> mappedWedgeWithRewards)
        {
            decimal startIndex = 0.00m;
            var finalWedge = new Dictionary<int, WedgeWithRange>();

            foreach (var item in mappedWedgeWithRewards)
            {
                var chance = (decimal)item.Value.Probability;

                if (chance == 0) continue;

                var start = startIndex;
                var end = startIndex + chance - 1.0m;
                var a = new WedgeWithRange
                {
                    Start = Math.Round(start, 1),
                    End = Math.Round(end, 1),
                    Probability = item.Value.Probability,
                    RewardType = item.Value.RewardType,
                    Slope = item.Value.Slope
                };

                finalWedge.Add(item.Key, a);
                startIndex = end + 0.1m;
            }

            return finalWedge;
        }
    }
}
