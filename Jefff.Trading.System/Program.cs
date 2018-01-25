using Akka.Actor;
using Jefff.Trading.System.Models;
using Jefff.Trading.System.StockBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Trading.System
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the ActorSystem
            var tradingSystem = ActorSystem.Create("TradingSystem");
            //Create the StockBroker
            var broker = tradingSystem.ActorOf(Props.Create(() => new StockBrockerActor()), "MyBroker");
            //Create the Trade message
            var trade = new Trade("ASUS", 100, TradeType.Sell, TradeStatus.Open, Guid.NewGuid());
            //Send the Trade message to the broker by way of an Ask(message). We want the underlying Task. 
            //Notice also that in Ask we are providing the Trade message as a generic type. This
            //is how the Task knows how to pass the response Trade message out to the caller.
            var task = broker.Ask<Trade>(trade);
            //Wait for the Task to complete. The task is completed upon the Broker receiving
            //response Trade message.
            task.Wait();
            //Return the response Trade message.
            var response = task.Result;

            Console.Read();
        }
    }
}
