using Akka.Actor;
using Jefff.Random.Model;
using Jefff.Random.RestApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomApi _randomApi = new RandomApi();
            var randomSystem = ActorSystem.Create("RandomStuffActor");
            var restActor = randomSystem.ActorOf(Props.Create(() => new RestApi.RestActor.RestActor(_randomApi)));
            var masterJedi = randomSystem.ActorOf(Props.Create(() => new MasterJediActor.MasterJediActor(restActor)), "Obi-Wan");
            Console.WriteLine("Enter a value to start, an integer is required!");
            while (true)
            {
                var value = Console.ReadLine();
                if (value.ToString() == string.Empty)
                {
                    value = "1";
                }
                var j = Convert.ToInt32(value);
                Console.WriteLine($"J is a {j.GetType()} and value is: {j}");
                var message = new MathActor.MathModel(j);
                masterJedi.Tell(message);
            }
        }
    }
}
