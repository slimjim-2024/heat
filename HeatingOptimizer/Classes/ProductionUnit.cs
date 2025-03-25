using System.Collections.Generic;

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

    public ProductionUnit(string name, double maxHeatOutput, double maxElectricity, decimal productionCosts, int co2Emissions, double consumption)
    {
        Name = name;
        MaxHeatOutput = maxHeatOutput;
        MaxElectricity = maxElectricity;
        ProductionCosts = productionCosts;
        CO2Emissions = co2Emissions;
        Consumption = consumption;
    }
}