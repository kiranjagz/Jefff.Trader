using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Solid.Fruit
{
    public class Apple : Fruit
    {
        public override string GetColour()
        {
            return "Green";
        }

        public override string GetNoise()
        {
            return "Gruchhh";
        }
    }
}
