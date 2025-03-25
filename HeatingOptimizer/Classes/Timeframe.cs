using System;

namespace HeatingOptimizer;

public class Timeframe
{
    protected internal DateTime TimeFrom { get; }
    protected internal DateTime TimeTo { get; }
    protected internal double HeatDemand { get; }
    protected internal decimal ElectricityPrice { get; }

    public Timeframe(DateTime tFrom, DateTime tTo, double hDemand, decimal ePrice)
    {
        TimeFrom = tFrom;
        TimeTo = tTo;
        HeatDemand = hDemand;
        ElectricityPrice = ePrice;
    }    
}