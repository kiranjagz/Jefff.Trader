using Jefff.Own.Random.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jefff.Own.Random.Seed.SeedEngine;

namespace Jefff.Own.Random.NewFormula
{
    public interface IWedgeCalculation
    {
        Dictionary<int, Pot> DistributePot(double noRewardPercentage, Dictionary<int, Pot> seededWedges);

        Dictionary<int, Pot> AddinNoRewardsToPot(int maxWedges, double noRewardPercentage, Dictionary<int, Pot> newWedge);
    }
}
