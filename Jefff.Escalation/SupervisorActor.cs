using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jefff.Escalation
{
    public class SupervisorActor : UntypedActor
    {
        private IActorRef cOne;
        private IActorRef cTwo;

        public SupervisorActor()
        {
            cOne = Context.ActorOf(Props.Create<ChildActor>(), "cOne");
            cTwo = Context.ActorOf(Props.Create<ChildActor>(), "bTwo");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    switch (ex)
                    {
                        case ArithmeticException ae:
                            {
                                Console.WriteLine($"error: {ex.Message}");
                                return Directive.Resume;
                            }
                        case NullReferenceException nre:
                            return Directive.Restart;
                        case ArgumentException are:
                            return Directive.Stop;
                        default:
                            return Directive.Escalate;
                    }
                });
        }

        protected override void OnReceive(object message)
        {
            //if (message is Props p)
            //{
            //    var child = Context.ActorOf(p); // create child
            //    Sender.Tell(child); // send back reference to child actor
            //}

            cOne.Tell(message);
            cTwo.Tell(new NullReferenceException());
        }
    }
}
