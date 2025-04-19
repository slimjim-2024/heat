using System;
using SkiaSharp.HarfBuzz;

namespace HeatingOptimizer;

public class Result
{
    public double HeatProduced { get; set; }
    public double ElectricityProduced { get; set; }
    public decimal ProductionCosts { get; set; }
    public double CO2Emissions { get; set; }
    public double Consumption { get; set; }
    public DateTime Times { get; set; }

    public Result()
    {
        
    }

    public Result(double heatProduced, double electricityProduced, decimal productionCosts, double co2Emissions, double consumption, DateTime times)
    {
        HeatProduced = heatProduced;
        ElectricityProduced = electricityProduced;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        Consumption = consumption;
        Times = times;
    }
}