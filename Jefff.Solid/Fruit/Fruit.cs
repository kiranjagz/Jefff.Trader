using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Fruit
{
    public abstract class Fruit
    {
        public abstract string GetColour();

        public virtual string GetNoise()
        {
            return "shoossh";
        }
    }
}
