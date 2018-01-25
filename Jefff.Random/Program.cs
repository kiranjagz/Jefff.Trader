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

            for (int j = 1; j <= 10; j++)
            {
                //if (j % 2 == 0)
                //{
                //    var message = new MathActor.MathModel(j);
                //    masterJedi.Tell(message);
                //}
                //else
                //{
                    var message = new TriviaActor.TrivaModel(j);
                    masterJedi.Tell(message);
                //}
            }

            Console.Read();
        }
    }
}
