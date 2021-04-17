using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Gwtdo.Sample.Stocks
{
    [ExcludeFromCodeCoverage]
    public class Stock
    {
        private DateTime _timeToCloseTrading;
        
        public Dictionary<string, int> Shares { get; }
        public Dictionary<string, int> Orders { get; }

        public Stock()
        {
            Shares = new Dictionary<string, int>();
            Orders = new Dictionary<string, int>();
        }

        public void Buy(string stock, int quantity)
        {
            if (Shares.ContainsKey(stock))
                Shares[stock] = Shares[stock] + quantity;
            else
                Shares[stock] = quantity;
        }
        
        public void Sell(string stock, int quantity)
        {
            if (!Shares.ContainsKey(stock)) return;
            
            Shares[stock] = Shares[stock] - quantity;
            Orders[stock] = quantity;
        }

        public void SetTimeToCloseTrading(string dateTime, string pattern = "yyyy-MM-dd HH:mm:ss")
        {
            _timeToCloseTrading = DateTime.ParseExact(dateTime, pattern, CultureInfo.InvariantCulture);
        }
    }
}