using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;

namespace HeatingOptimizer;

public enum ProductionUnitType
{
    GasBoiler,
    OilBoiler,
    GasMotor,
    HeatPump
}

public class ProductionUnit
{
    public string Name { get; set; } = "";
    public double MaxHeatOutput { get; set; }
    public double MaxElectricity { get; set; }
    public decimal ProductionCosts { get; set; }
    public int CO2Emissions { get; set; }
    public double Consumption { get; set; }
    public SKColor Color { get; set; } = SKColor.Empty;
    public ProductionUnit() { }
}

