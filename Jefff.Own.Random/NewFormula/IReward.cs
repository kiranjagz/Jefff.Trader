using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Own.Random.NewFormula
{
    public interface IReward
    {
        KeyValuePair<int, WedgeWithRange> FindReward(Dictionary<int, WedgeWithRange> finalWedge, decimal trueRandom);
    }
}
