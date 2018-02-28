using Akka.Actor;
using Jefff.Random.Database.Mongo;
using Jefff.Random.Model;
using Jefff.Random.RestApi.RestActor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.TriviaActor
{
    public class TriviaActor : ReceiveActor
    {
        private readonly IActorRef _restActor;
        private readonly IList _collection;
        private readonly IMongoSetting _mongoSetting;
        private readonly IMongoRepository _mongoRepository;

        public TriviaActor(IActorRef restActor)
        {
            _mongoSetting = new MongoSetting();
            _mongoRepository = new MongoRepository(_mongoSetting);
            _restActor = restActor;
            _collection = new List<int>();
            Receive<TrivaModel>(message => HandleMessage(message));
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(2), Self, new TrivaModel.TimeTrigger(DateTime.Now), ActorRefs.Nobody);

            ReceiveAsync<TrivaModel.TimeTrigger>(HandleTimeTrigger);
        }

        private async Task HandleTimeTrigger(TrivaModel.TimeTrigger obj)
        {
            var count = _collection.Count;
            Console.WriteLine($"Timer has started with time: {obj.TriggerTime}");
            Console.WriteLine($"Count of items: {count}");
            foreach
                (var item in _collection)
            {
                var response = await HandleApiCall(new RestRequestModel(Convert.ToInt32(item), "trivia"));
                await _mongoRepository.Save(response);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            }

        }

        private async void HandleMessage(TrivaModel message)
        {
            try
            {
                _collection.Add(message.Number);
                var response = await HandleApiCall(new RestRequestModel(message.Number, "trivia"));
                await _mongoRepository.Save(response);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private async Task<ResponseModel> HandleApiCall(RestRequestModel model)
        {
            return await _restActor.Ask<ResponseModel>(model, TimeSpan.FromSeconds(40));
        }
    }
}
