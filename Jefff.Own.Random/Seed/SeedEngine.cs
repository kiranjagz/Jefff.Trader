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
    }
}
