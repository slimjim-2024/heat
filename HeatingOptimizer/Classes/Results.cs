using System;
using System.Collections.Generic;
using SkiaSharp;

namespace HeatingOptimizer;

public class Results
{
    public List<double> HeatProduced { get; set; } = [];
    public List<double> ElectricityProduced { get; set; } = [];
    public List<decimal> ProductionCosts { get; set; } = [];
    public List<double> CO2Emissions { get; set; } = [];
    public List<double> Consumption { get; set; } = [];
    public List<DateTime> Times { get; set; } = [];
}