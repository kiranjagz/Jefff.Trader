using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.MathActor;
using Jefff.Random.TriviaActor;
using Akka.Routing;
using Jefff.Random.RestApi.Model;
using Jefff.Random.MasterJediActor.ChildActors.MathActor.MathService;
using Jefff.Random.MasterJediActor.ChildActors.TriviaActor.TriviaService;
using Jefff.Random.RestApi;

namespace Jefff.Random.MasterJediActor
{
    public class MasterJediActor : ReceiveActor
    {
        private readonly IActorRef _mathActor;
        private readonly IActorRef _triviaActor;
        private readonly IRandomApi _randomApi;

        public MasterJediActor()
        {
            _randomApi = new RandomApi();
            _mathActor = Context.ActorOf(Props.Create(() => new ChildActors.MathActor.MathActor(new MathService(_randomApi)))
                .WithRouter(new ConsistentHashingPool(2).WithHashMapping(x =>
                {
                    if (x is ConsistentHashableEnvelope envelope)
                        return envelope.ConsistentHashKey;
                    return x;
                })), "Jeff_Math");

            _triviaActor = Context.ActorOf(Props.Create(() => new ChildActors.TriviaActor.TriviaActor(new TriviaService(_randomApi))), "Bobb_Triva");
            Receive<MathModel>(message => HandleMathFact(message));
            Receive<TriviaModel>(message => HandleTrivaFact(message));

            //Handles messages from child actors...
            Receive<TriviaResultModel>(r => HandleTriviaResult(r));
            Receive<MathResultModel>(r => HandleMathResult(r));
        }

        private void HandleMathResult(MathResultModel r)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(r));
            Console.WriteLine($"Parent is: {Context.Self}");
            Console.WriteLine($"Child is: {Context.Sender.Path}");
            Console.WriteLine($"Work it has, done well you have!");
        }

        private void HandleTriviaResult(TriviaResultModel r)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(r));
            Console.WriteLine($"Parent is: {Context.Self}");
            Console.WriteLine($"Child is: {Context.Sender.Path}");
            Console.WriteLine($"Work it has, done well you have!");
        }

        private void HandleTrivaFact(TriviaModel message)
        {
            _triviaActor.Tell(message);
        }

        private void HandleMathFact(MathModel message)
        {
            var env = new ConsistentHashableEnvelope(message, message.Number);
            _mathActor.Tell(env);
        }

        protected override void PreStart()
        {
            Console.WriteLine($"Jedi Master Actor has started has it!");
            base.PreStart();
        }
    }
}
