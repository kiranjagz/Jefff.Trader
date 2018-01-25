using Akka.Actor;
using Jefff.Trading.System.FloorTrader;
using Jefff.Trading.System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Trading.System.StockBroker
{
    public class StockBrockerActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var trade = message as Trade;

            if (trade != null)
            {
                //Make sure you are processing only open trades
                if (trade.TradeStatus.Equals(TradeStatus.Open))
                {
                    if (trade.TradeType.Equals(TradeType.Sell))
                    {
                        //create the Sell FloorTrader and forward the message
                        var sellTrader = Context.ActorOf(Props.Create(() => new FloorTraderActor()), "SellFloorTrader");
                        sellTrader.Forward(trade);
                    }
                    else
                    {
                        //create the Buy FloorTrader and forward the message
                        var buyTrader = Context.ActorOf(Props.Create(() => new FloorTraderActor()), "BuyFloorTrader");
                        buyTrader.Forward(trade);
                    }
                }
            }
        }
    }
}
