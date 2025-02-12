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
    double GasConsumption { get; }
    double OilConsumption { get; }  
    
    
    protected internal ProductionUnit(ProductionUnitType type, string name, double maxHeatOutput, double maxElectricity, decimal productionCosts, int co2Emissions)
    {
        Type = type;
        Name = name;
        MaxHeatOutput = maxHeatOutput;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        
    }
}