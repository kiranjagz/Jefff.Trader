using Akka.Actor;
using Jefff.Random.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.MathActor
{
    public class MathActor : ReceiveActor
    {
        private readonly IActorRef _restActor;

        public MathActor(IActorRef restActor)
        {
            _restActor = restActor;
            ReceiveAsync<MathModel>(message => HandleMessage(message));
        }

        private async Task HandleMessage(MathModel message)
        {
            try
            {
                var response = await _restActor.Ask<ResponseModel>(new RestApi.RestActor.RestRequestModel(message.Number, "math"));
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
                Console.WriteLine($"Parent is: {Context.Parent.Path}");
                Console.WriteLine($"Router is: {Context.Self.Path} ");
                Console.WriteLine("--------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
