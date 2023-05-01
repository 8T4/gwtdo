using System.Diagnostics.CodeAnalysis;

namespace Gwtdo.Sample;

[ExcludeFromCodeCoverage]
public class Trading
{
    public Dictionary<string, int> Shares { get; } = new();
    public Dictionary<string, int> Orders { get; } = new();
    public TradingClock Clock { get; }

    public Trading(TradingClock? clock = null)
    {
        Clock = clock ?? new TradingClock();
    }

    public void Buy(TradingOrder order)
    {
        if (!Clock.IsBeforeCloseOfTrading(order)) return;
        
        if (Shares.ContainsKey(order.Asset))
            Shares[order.Asset] += order.Quantity;
        else
            Shares[order.Asset] = order.Quantity;
    }

    public void Sell(TradingOrder order)
    {
        if (!Clock.IsBeforeCloseOfTrading(order)) return;

        if (!Shares.ContainsKey(order.Asset))
            return;

        Shares[order.Asset] -= order.Quantity;
        UpdateOrders(order);
    }

    private void UpdateOrders(TradingOrder order)
    {
        if (Orders.ContainsKey(order.Asset))
            Orders[order.Asset] += order.Quantity;
        else
            Orders[order.Asset] = order.Quantity;
    }
}