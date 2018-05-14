using Akka.Actor;
using Jefff.Random.RestApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jefff.Random
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomApi _randomApi = new RandomApi();
            var randomSystem = ActorSystem.Create("RandomStuffActor");

            var masterJedi = randomSystem.ActorOf(Props.Create(() => new MasterJediActor.MasterJediActor()), "Obi-Wan");

            while (true)
            {
                Console.WriteLine("Enter a value to start, an integer is required!");
                var value = Console.ReadLine();
                if (value == string.Empty)
                    value = "1";

                var j = Convert.ToInt32(value);
                object message;
                if (j % 2 == 0)
                    message = new MathActor.MathModel(j);
                else
                    message = new TriviaActor.TriviaModel(j);
                Console.WriteLine($"J is a {message.GetType()} and value is: {j}");
                masterJedi.Tell(message);

                Console.WriteLine("Sleeping for 10 seconds then continue :)");
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
    }
}
