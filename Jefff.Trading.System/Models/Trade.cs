using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Trading.System.Models
{
    public class Trade
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="ticker">The stock ticker symbol</param>
        /// <param name="shares">The number of shares to trade</param>
        /// <param name="tradeTradeType">TradeType.Buy or TradeType.Sell</param>
        /// <param name="tradeStatus">
        ///     A TradeStatus enum that describes the status of the
        ///     Trade
        /// </param>
        /// <param name="tradingSessionId">
        ///     Each trading session is given a unique identifier of type, Guid.
        ///     Either create a TradingSessionId if you are starting a trade. Or pass in the existing
        ///     TradingSessionId if you are part of a trade.
        /// </param>
        /// <param name="message">An arbitrary message</param>
        public Trade(string ticker, int shares, TradeType tradeTradeType, TradeStatus tradeStatus,
            Guid tradingSessionId, string message = null)
        {
            Ticker = ticker;
            Shares = shares;
            TradeType = tradeTradeType;
            TradingSessionId = tradingSessionId;
            CreateTime = DateTime.UtcNow;
            TradeStatus = tradeStatus;
            Message = message;
        }

        public string Ticker { get; private set; }

        public int Shares { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType TradeType { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TradeStatus TradeStatus { get; private set; }

        public Guid TradingSessionId { get; private set; }

        public DateTime CreateTime { get; private set; }

        public string Message { get; private set; }
    }

    public enum TradeType
    {
        Buy,
        Sell
    }

    public enum TradeStatus
    {
        Initialize,
        Open,
        Success,
        Fail
    }
}
