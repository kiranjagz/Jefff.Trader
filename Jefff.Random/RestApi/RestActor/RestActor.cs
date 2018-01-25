using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.RestApi.RestActor
{
    public class RestActor : ReceiveActor
    {
        private RandomApi _randomApi;

        public RestActor(RandomApi randomApi)
        {
            ReceiveAsync<RestRequestModel>(message => GetFacts(message));
            _randomApi = randomApi;
        }

        private async Task GetFacts(RestRequestModel message)
        {
            var response = await _randomApi.FactGet(message.Number, message.Type);
            Console.WriteLine($"Called Api $$$");
            Sender.Tell(response, Self);
        }
    }
}
