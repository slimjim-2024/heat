using System;
using SkiaSharp.HarfBuzz;

namespace HeatingOptimizer;

public class Result
{
    public double HeatProduced { get; set; }
    public double ElectricityProduced { get; set; }
    public decimal ProductionCosts { get; set; }
    public decimal Revenue { get; set; }
    public double CO2Emissions { get; set; }
    public double Consumption { get; set; }
    public DateTime TimeFrom { get; set; }

    public Result()
    {
        
    }

    public Result(double heatProduced, double electricityProduced, decimal productionCosts, decimal revenue, double co2Emissions, double consumption, DateTime times)
    {
        HeatProduced = heatProduced;
        ElectricityProduced = electricityProduced;
        ProductionCosts = productionCosts;
        Revenue = revenue;
        CO2Emissions = co2Emissions;
        Consumption = consumption;
        TimeFrom = times;
    }
}