using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.MathActor;
using Jefff.Random.TriviaActor;

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
            _mathActor = Context.ActorOf(Props.Create(() => new MathActor.MathActor(_restActor)),"Jefff_Math");
            _triviaActor = Context.ActorOf(Props.Create(() => new TriviaActor.TriviaActor(_restActor)),"Bobb_Triva");
            Receive<MathModel>(message => HandleMathFact(message));
            Receive<TrivaModel>(message => HandleTrivaFact(message));
        }

        private void HandleTrivaFact(TrivaModel message)
        {
            _triviaActor.Tell(message);
        }

        private void HandleMathFact(MathModel message)
        {
            _mathActor.Tell(message);
        }

        protected override void PreStart()
        {
            base.PreStart();
        }
    }
}
