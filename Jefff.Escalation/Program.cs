using Akka.Actor;
using System;

namespace Jefff.Escalation
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("jefff");

            var supervisor = actorSystem.ActorOf(Props.Create(() => new SupervisorActor()), "SuperDuper");

            for (int i = 0; i <= 10; i++)
            {
                supervisor.Tell(1);
            }
            //var child = ExpectMsg<IActorRef>(); 

            Console.Read();
        }
    }
}
