using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Jefff.Random.MasterJediActor.ChildActors.TriviaActor.TriviaService;
using Jefff.Random.RestApi;
using Jefff.Random.TriviaActor;

namespace Jefff.Random.MasterJediActor.ChildActors.TriviaActor
{
    public class TriviaActor : ReceiveActor
    {
        private static readonly ICollection<int> _collection;
        private readonly ITriviaService _triviaService;

        static TriviaActor()
        {
            _collection = new List<int>();
        }

        public TriviaActor(ITriviaService triviaService)
        {
            _triviaService = triviaService;

            ReceiveAsync<TriviaModel>(HandleMessageAsync);
            Receive<TriviaModel.TimeTrigger>(x => HandleTimeTrigger(x));
            Receive<TriviaModel.TimeTriggerOnce>(x => HandleTimeTriggerOnce(x));

            Context.System.Scheduler.ScheduleTellRepeatedly(
              TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1),
              Self,
              new TriviaModel.TimeTrigger(DateTime.Now), ActorRefs.Nobody);


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
                Console.WriteLine($"Number was used: {item}");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void HandleTimeTriggerOnce(TriviaModel.TimeTriggerOnce trigger)
        {
            var v = trigger.Message;

            Console.WriteLine($"Timer one has started with time: {trigger.Message}");
        }

        private async Task HandleMessageAsync(TriviaModel message)
        {
            //_collection.Add(message.Number);
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(60), Self, new TriviaModel.TimeTriggerOnce(DateTime.Now, message.Number.ToString()), ActorRefs.Nobody);

            await _triviaService.DoApiWork(new RestRequestModel(message.Number, "trivia")).PipeTo(Sender);
        }
    }
}
