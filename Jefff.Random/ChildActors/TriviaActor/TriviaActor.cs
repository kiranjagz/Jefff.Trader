using Akka.Actor;
using Jefff.Random.Model;
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

        public TriviaActor(IActorRef restActor)
        {
            _restActor = restActor;
            _collection = new List<int>();
            Receive<TrivaModel>(message => HandleMessage(message));
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), Self, new TrivaModel.TimeTrigger(DateTime.Now), ActorRefs.Nobody);

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
                var response =
                    await _restActor.Ask<ResponseModel>(new RestApi.RestActor.RestRequestModel(Convert.ToInt32(item), "trivia"));
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response));
            }

        }

        private async void HandleMessage(TrivaModel message)
        {
            try
            {
                _collection.Add(message.Number);
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
