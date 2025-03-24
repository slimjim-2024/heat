using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

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
    public string Name { get; }
    public double MaxHeatOutput { get; }
    public double MaxElectricity { get; }
    public decimal ProductionCosts { get; }
    public int CO2Emissions { get; }
    public double Consumption { get; }

    public List<double> SeasonHeatProduction = [];
    public List<double> SeasonElectricityProduction = [];
    public List<decimal> SeasonProductionCosts = [];

    public ProductionUnit(string name, double? maxHeatOutput, double? maxElectricity, decimal? productionCosts, int? co2Emissions, double? consumption)
    {
        Name = name;
        MaxHeatOutput = maxHeatOutput ?? 0.0d;
        MaxElectricity = maxElectricity ?? 0.0d;
        ProductionCosts = productionCosts ?? 0.0m;
        CO2Emissions = co2Emissions ?? 0;
        Consumption = consumption ?? 0;
    }
     public static ProductionUnit CreateProductionUnit(ProductionUnitType type)
        {
            return type switch
            {
                ProductionUnitType.GasBoiler => new ProductionUnit("Gas Boiler", 1000.0, 0.0, 500.0m, 200, 150.0),
                ProductionUnitType.OilBoiler => new ProductionUnit("Oil Boiler", 800.0, 0.0, 450.0m, 250, 120.0),
                ProductionUnitType.GasMotor => new ProductionUnit("Gas Motor", 500.0, 200.0, 350.0m, 150, 100.0),
                ProductionUnitType.HeatPump => new ProductionUnit("Heat Pump", 700.0, 250.0, 400.0m, 50, 80.0),
                _ => throw new ArgumentException("Invalid production unit type")
            };
        }
}