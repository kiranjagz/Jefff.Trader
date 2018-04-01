using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.TriviaActor
{
    public class TriviaModel
    {
        public TriviaModel(int number)
        {
            Number = number;
        }
        public int Number { get; }
        public DateTime DateTimeCreated { get; } = DateTime.Now;

        public class TimeTrigger
        {
            public DateTime TriggerTime { get; }
            public TimeTrigger(DateTime triggerTime)
            {
                TriggerTime = triggerTime;
            }
        }
    }


}
