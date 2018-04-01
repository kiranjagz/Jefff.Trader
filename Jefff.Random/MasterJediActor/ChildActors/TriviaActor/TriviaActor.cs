using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Jefff.Random.Database.Mongo;
using Jefff.Random.Model;
using Jefff.Random.RestApi.RestActor;
using Jefff.Random.TriviaActor;

namespace Jefff.Random.MasterJediActor.ChildActors.TriviaActor
{
    public class TriviaActor : ReceiveActor
    {
        private readonly IActorRef _restActor;
        private readonly ICollection<int> _collection;
        private readonly IMongoSetting _mongoSetting;
        private readonly IMongoRepository _mongoRepository;

        public TriviaActor(IActorRef restActor)
        {
            _mongoSetting = new MongoSetting();
            _mongoRepository = new MongoRepository(_mongoSetting);
            _restActor = restActor;
            _collection = new HashSet<int>();
            ReceiveAsync<TriviaModel>(HandleMessage);
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), Self, new TriviaModel.TimeTrigger(DateTime.Now), ActorRefs.Nobody);

            ReceiveAsync<TriviaModel.TimeTrigger>(HandleTimeTrigger);
        }

        private async Task HandleTimeTrigger(TriviaModel.TimeTrigger trigger)
        {
            var count = _collection.Count;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Timer has started with time: {trigger.TriggerTime}");
            Console.WriteLine($"Count of items: {count}");
            foreach
                (var item in _collection)
            {
                var response = await HandleApiCall(new RestRequestModel(Convert.ToInt32(item), "trivia"));
                var returnedDocument = await _mongoRepository.FindOneAndReplace(response);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(returnedDocument));
            }

        }

        private async Task HandleMessage(TriviaModel message)
        {
            _collection.Add(message.Number);
            var response = await HandleApiCall(new RestRequestModel(message.Number, "trivia"));
            await _mongoRepository.Save(response);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            Console.WriteLine($"Parent is: {Context.Parent.Path}");
            Console.WriteLine($"Router is: {Context.Self.Path} ");
            Console.WriteLine("--------------------------------");

        }

        private async Task<ResponseModel> HandleApiCall(RestRequestModel model)
        {
            return await _restActor.Ask<ResponseModel>(model, TimeSpan.FromSeconds(40));
        }
    }
}
