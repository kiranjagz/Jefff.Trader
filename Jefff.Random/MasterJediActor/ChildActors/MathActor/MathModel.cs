using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.MathActor
{
    public class MathModel : IConsistentHashable
    {
        public MathModel(int number)
        {
            _number = number;
        }
        readonly int _number;
        public int Number { get { return _number; } }
        public DateTime DateTimeCreated { get; } = DateTime.Now;

        public object ConsistentHashKey
        {
            get
            {
                return _number;
            }
        }
    }
}
