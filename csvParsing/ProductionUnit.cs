using System.Reflection.Metadata;

namespace HeatingOptimizer;
enum ProductionUnitType
{
    GasBoiler,
    OilBoiler,
    GasMotor,
    HeatPump
}
class ProductionUnit
{
    protected internal string Name { get; }
    protected internal double MaxHeatOutput { get; }
    protected internal double MaxElectricity { get; }
    protected internal decimal ProductionCosts { get; }
    protected internal int CO2Emissions { get; }
    protected internal double Consumption { get; }

    protected internal List<double> SeasonHeatProduction = new();
    protected internal List<double> SeasonElectricityProduction = new();
    protected internal List<decimal> SeasonProductionCosts = new();

    protected internal ProductionUnit(string name, double? maxHeatOutput, double? maxElectricity, decimal? productionCosts, int? co2Emissions, double? consumption)
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