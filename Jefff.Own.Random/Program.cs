using Jefff.Own.Random.Extensions;
using Jefff.Own.Random.NewFormula;
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
        private const double noRewardPerc = 40.0;
        private static IWedgeCalculation _wedgeCalculation = new WedgeCalculation();
        private static IWedgePlacement _wedgePlacment = new WedgePlacement();
        private static IReward _reward = new Reward();

        static void Main(string[] args)
        {
            var seedWedges = SeedEngine.SeedPot();
            var seedRewards = SeedEngine.SeedRewardTypes();

            var postSeedReward = seedRewards.OrderBy(x => TrueRandomStuff.TrueRandom(0, seedRewards.Where(y => y.Value == 1).Count())).OrderBy
                (x => x.Value).ToList();

            var newWedge = _wedgeCalculation.DistributePot(noRewardPerc, seedWedges);
            var newWedgeIncludingNoRewards = _wedgeCalculation.AddinNoRewardsToPot(seedRewards.Count, noRewardPerc, newWedge);

            var wedgePlacment = _wedgePlacment.MapWedgeRewards(newWedgeIncludingNoRewards, postSeedReward);

            var finalWedge = _wedgePlacment.FinalWedgeWithRange(wedgePlacment);
            var w = finalWedge.Count();
            var z = finalWedge[w].End;
            decimal trueRandom = TrueRandomStuff.TrueRandom(0, (int)z);
            Console.WriteLine($"Random calculation for engine to compute: {trueRandom}");

            var findReward = _reward.FindReward(finalWedge, trueRandom);

            Console.WriteLine($"===================="); ;
            Console.Read();
        }
    }
}
