using Akka.Actor;
using Jefff.Trading.System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Trading.System.FloorTrader
{
    public class FloorTraderActor : UntypedActor
    {
        private const int TradingLimit = 200;

        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null)
            {
                Trade responseTrade;
                if (trade.Shares > TradingLimit)
                {
                    var msg =
                        string.Format("You want to trade {0} Shares. The trade exceeds the trading limit of {1}. The system will not {2} {0} of {3}.",
                            trade.Shares, TradingLimit, trade.TradeType, trade.Ticker);
                    responseTrade = new Trade(trade.Ticker, trade.Shares, trade.TradeType, TradeStatus.Fail, trade.TradingSessionId, msg);
                }
                else
                {
                    var msg = string.Format("I am {0}ing {1} shares of {2}", trade.TradeType, trade.Shares, trade.Ticker);
                    responseTrade = new Trade(trade.Ticker, trade.Shares, trade.TradeType, TradeStatus.Success, trade.TradingSessionId, msg);
                }
                var whatIsThis = Self;
                Sender.Tell(responseTrade, Self);
            }
        }
    }
}
