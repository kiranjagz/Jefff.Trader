using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jefff.Escalation
{
    public class ChildActor : UntypedActor
    {
        private int state = 0;

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case Exception ex:
                    throw ex;
                    break;
                case int x:
                    state = x;
                    break;
                case "get":
                    Sender.Tell(state);
                    break;
            }
        }
    }
}
