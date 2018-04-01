using Akka.Actor;
using Jefff.Random.Database.Mongo;
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
        //private readonly IMongoRepository _mongoRepository;
        //private readonly IMongoSetting _mongoSetting;

        public MathActor(IActorRef restActor)
        {
            //_mongoRepository = new MongoRepository(_mongoSetting);
            _restActor = restActor;
            ReceiveAsync<MathModel>(message => HandleMessage(message));
        }

        private async Task HandleMessage(MathModel message)
        {
            var response = await _restActor.Ask<ResponseModel>(new RestApi.RestActor.RestRequestModel(message.Number, "math"));
            //await _mongoRepository.Save(response);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            Console.WriteLine($"Parent is: {Context.Parent.Path}");
            Console.WriteLine($"Router is: {Context.Self.Path} ");
            Console.WriteLine("--------------------------------");
        }
    }
}
