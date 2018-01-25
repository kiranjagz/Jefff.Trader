using Akka.Actor;
using Jefff.Random.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.TriviaActor
{
    public class TriviaActor : ReceiveActor
    {
        private readonly IActorRef _restActor;

        public TriviaActor(IActorRef restActor)
        {
            _restActor = restActor;
            Receive<TrivaModel>(message => HandleMessage(message));
        }

        private async void HandleMessage(TrivaModel message)
        {
            try
            {
                var response = await _restActor.Ask<ResponseModel>(new RestApi.RestActor.RestRequestModel(message.Number, "trivia"));
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
