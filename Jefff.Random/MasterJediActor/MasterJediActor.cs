using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.MathActor;
using Jefff.Random.TriviaActor;
using Akka.Routing;

namespace Jefff.Random.MasterJediActor
{
    public class MasterJediActor : ReceiveActor
    {
        private readonly IActorRef _mathActor;
        private readonly IActorRef _triviaActor;
        private readonly IActorRef _restActor;


        public MasterJediActor(IActorRef restActor)
        {
            _restActor = restActor;
            _mathActor = Context.ActorOf(Props.Create(() => new MathActor.MathActor(_restActor))
                .WithRouter(new ConsistentHashingPool(2).WithHashMapping(x =>
                {
                    if (x is ConsistentHashableEnvelope)
                        return ((ConsistentHashableEnvelope)x).ConsistentHashKey;
                    return x;
                })), "Jeff_Math");


            _triviaActor = Context.ActorOf(Props.Create(() => new ChildActors.TriviaActor.TriviaActor(_restActor)), "Bobb_Triva");
            Receive<MathModel>(message => HandleMathFact(message));
            Receive<TriviaModel>(message => HandleTrivaFact(message));
        }

        private void HandleTrivaFact(TriviaModel message)
        {
            _triviaActor.Tell(message);
        }

        private void HandleMathFact(MathModel message)
        {
            var math = new MathModel(message.Number);
            var env = new ConsistentHashableEnvelope(message, message.Number);
            _mathActor.Tell(env);
        }

        protected override void PreStart()
        {
            base.PreStart();
        }
    }
}
