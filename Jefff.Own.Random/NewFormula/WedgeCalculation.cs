using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Own.Random.Seed;
using static Jefff.Own.Random.Seed.SeedEngine;

namespace Jefff.Own.Random.NewFormula
{
    public class WedgeCalculation : IWedgeCalculation
    {
        public Dictionary<int, Pot> AddinNoRewardsToPot(int maxWedges, double noRewardPercentage, Dictionary<int, Pot> newWedge)
        {
            var maxRewardRange = maxWedges;
            var rewardRange = newWedge.Count();
            var noRewardRange = maxRewardRange - rewardRange;

            var noRewardMultiplerPerc = Math.Round(noRewardPercentage / noRewardRange, 1, MidpointRounding.AwayFromZero);
            var lastRewardCount = newWedge.OrderByDescending(x => x.Key).FirstOrDefault().Key;

            var index = lastRewardCount;
            for (int i = 0; i < noRewardRange; i++)
            {
                index++;
                double newProb = Math.Round(noRewardMultiplerPerc, 1, MidpointRounding.AwayFromZero);
                var newPot = new Pot { Probability = newProb, Slope = 0.00m };
                newWedge.Add(index, newPot);

            }

            return newWedge;
        }

        public Dictionary<int, Pot> DistributePot(double noRewardPercentage, Dictionary<int, Pot> seededWedges)
        {
            var noRewardPerc = noRewardPercentage;
            var reminder = 100.0 - noRewardPerc;

            var newWedge = new Dictionary<int, SeedEngine.Pot>();

            foreach (var item in seededWedges.OrderBy(x => x.Key))
            {
                var wedge = item.Key;
                double newProb = Math.Round(item.Value.Probability * reminder / 100, 1, MidpointRounding.AwayFromZero);
                var newPot = new Pot { Probability = newProb, Slope = item.Value.Slope };
                newWedge.Add(item.Key, newPot);
            }

            var sumWedge = newWedge.Values.Sum(x => x.Probability);

            if (sumWedge != 100)
            {
                var diff = 100 - sumWedge;
            }

            return newWedge;
        }
    }
}
