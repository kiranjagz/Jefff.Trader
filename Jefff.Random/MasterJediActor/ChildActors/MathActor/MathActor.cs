using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Jefff.Random.MasterJediActor.ChildActors.MathActor.MathService;
using Jefff.Random.MathActor;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.MasterJediActor.ChildActors.MathActor
{
    public class MathActor : ReceiveActor
    {
        private readonly IMathService _mathService;

        public MathActor(IMathService mathService)
        {
            _mathService = mathService;
            ReceiveAsync<MathModel>(HandleMessageAsync);
            ReceiveAsync<ConsistentHashableEnvelope>(HandleMessageWithHashAsync);
        }

        private async Task HandleMessageWithHashAsync(ConsistentHashableEnvelope arg)
        {
            var message = arg.Message as MathModel;

            await _mathService.DoApiWork(new RestRequestModel(message.Number, "math")).PipeTo(Sender, Self);
        }

        private async Task HandleMessageAsync(MathModel message)
        {
            await _mathService.DoApiWork(new RestRequestModel(message.Number, "math")).PipeTo(Sender, Self);
        }
    }
}
