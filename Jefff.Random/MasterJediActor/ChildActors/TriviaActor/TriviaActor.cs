using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Jefff.Random.Database.Mongo;
using Jefff.Random.Database.Mongo.Setting;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;
using Jefff.Random.RestApi.RestActor;
using Jefff.Random.TriviaActor;

namespace Jefff.Random.MasterJediActor.ChildActors.TriviaActor
{
    public class TriviaActor : ReceiveActor
    {
        private readonly ICollection<int> _collection;

        public TriviaActor(IActorRef restActor)
        {
            _collection = new HashSet<int>();
            ReceiveAsync<TriviaModel>(HandleMessage);
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), Self, new TriviaModel.TimeTrigger(DateTime.Now), ActorRefs.Nobody);

            ReceiveAsync<TriviaModel.TimeTrigger>(HandleTimeTrigger);
        }

        private void HandleTimeTrigger(TriviaModel.TimeTrigger trigger)
        {
            var count = _collection.Count;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Timer has started with time: {trigger.TriggerTime}");
            Console.WriteLine($"Count of items: {count}");
            foreach
                (var item in _collection)
            {
            }

        }

        private async Task HandleMessage(TriviaModel message)
        {
            _collection.Add(message.Number);

        }
    }
}
