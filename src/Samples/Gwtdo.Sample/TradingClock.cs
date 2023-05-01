namespace Gwtdo.Sample;

public class TradingClock
{
    public DateTime CurrentDateTime { get; }
    public DateTime LimitDateTime { get; private set; }

    public TradingClock(DateTime? limit = null)
    {
        CurrentDateTime = DateTime.Now;
        LimitDateTime = limit ?? DateTime.Today.AddHours(18);
    }

    public void UpdateLimit(DateTime limit)
        => LimitDateTime = limit;

    public bool IsBeforeCloseOfTrading()
        => (CurrentDateTime.Date == LimitDateTime.Date) && (CurrentDateTime < LimitDateTime);

    public bool IsBeforeCloseOfTrading(TradingOrder order)
        => (order.OrderDate.Date == LimitDateTime.Date) && (order.OrderDate < LimitDateTime);
}