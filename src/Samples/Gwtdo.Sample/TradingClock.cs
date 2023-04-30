namespace Gwtdo.Sample;

public class TradingClock
{
    public DateTime CurrentDateTime { get; }
    public DateTime LimitDateTime { get; }

    public TradingClock(DateTime? currentDateTime = null, DateTime? limit = null)
    {
        CurrentDateTime = currentDateTime ?? DateTime.Now;
        LimitDateTime = limit ?? DateTime.Today.AddHours(18);
    }
}