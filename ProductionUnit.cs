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
    ProductionUnitType Type { get; }
    string Name { get; }
    double MaxHeatOutput { get; }
    double MaxElectricity { get; }
    decimal ProductionCosts { get; }
    int CO2Emissions { get; }
    double Consumption { get; }
    
    
    protected internal ProductionUnit(ProductionUnitType type, string name, double? maxHeatOutput, double? maxElectricity, decimal? productionCosts, int? co2Emissions, double? consumption)
    {
        Type = type;
        Name = name;
        MaxHeatOutput = maxHeatOutput ?? 0.0d;
        MaxElectricity = maxElectricity ?? 0.0d;
        ProductionCosts = productionCosts ?? 0.0m;
        CO2Emissions = co2Emissions ?? 0;
        Consumption = consumption ?? 0;
    }
}