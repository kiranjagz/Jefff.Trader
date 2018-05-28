using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jefff.Own.Random.Program;
using static Jefff.Own.Random.Seed.SeedEngine;

namespace Jefff.Own.Random.NewFormula
{
    public interface IWedgePlacement
    {
        Dictionary<int, Wedge> MapWedgeRewards(Dictionary<int, Pot> newWedgeIncludingNoRewards,
            List<KeyValuePair<string, int>> randomOrderedRewards);

        Dictionary<int, WedgeWithRange> FinalWedgeWithRange(Dictionary<int, Wedge> mappedWedgeWithRewards);

    }
}
