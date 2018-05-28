using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random.Seed
{
    public static class SeedEngine
    {
        public static Dictionary<string, int> SeedData()
        {
            var seededData = new Dictionary<string, int>();

            seededData.Add("FreeBonus", 25);
            seededData.Add("FreeCash", 5);
            seededData.Add("FreeSpins", 10);
            seededData.Add("FreeStuff", 10);
            seededData.Add("NoReward", 50);

            return seededData;
        }

        public static Dictionary<int, int> SeedWedges()
        {
            var seededData = new Dictionary<int, int>();

            seededData.Add(1, 27);
            seededData.Add(2, 24);
            seededData.Add(3, 20);
            seededData.Add(4, 17);
            seededData.Add(5, 12);

            return seededData;
        }

        public static Dictionary<int, Pot> SeedPot()
        {
            var seededData = new Dictionary<int, Pot>();

            seededData.Add(1, new Pot { Probability = 27, Slope = 0.64m });
            seededData.Add(2, new Pot { Probability = 24, Slope = 0.81m });
            seededData.Add(3, new Pot { Probability = 20, Slope = 1.02m });
            seededData.Add(4, new Pot { Probability = 17, Slope = 1.24m });
            seededData.Add(5, new Pot { Probability = 12, Slope = 1.60m });

            return seededData;
        }

        public static Dictionary<string, int> SeedRewardTypes()
        {
            var seededData = new Dictionary<string, int>();

            seededData.Add("FreeBonus", 1);
            seededData.Add("FreeCash", 1);
            seededData.Add("FreeSpins", 1);
            seededData.Add("LoyaltyPoints", 1);
            seededData.Add("Boost", 1);
            seededData.Add("TimerReduction", 2);
            seededData.Add("SpinAgain", 2);
            seededData.Add("NoReward", 2);

            return seededData;
        }

        public class Pot
        {
            public double Probability { get; set; }
            public decimal Slope { get; set; }
        }
    }
}
