namespace HeatingOptimizer;

public static class ProdUnitSorter
{
    /*
    public enum SortType
    {
        Cheapest,
        MoreGasEfficient(LessConsumption),
        LessCO2Emissions
    }
    */

    public static List<ProductionUnit> Sort(List<ProductionUnit> pUnits, decimal electricityPrice, short sortType)
    {
        return sortType switch
        {
            // (Heat cost - Electricity return)/Total heat production = cost/return per unit of heat
            0 => [.. pUnits.OrderBy(x => (x.MaxHeatOutput * (double)x.ProductionCosts - x.MaxElectricity * (double)electricityPrice) / x.MaxHeatOutput)],
            1 => [.. pUnits.OrderBy(x => x.CO2Emissions)],// Less emissions
            2 => [.. pUnits.OrderBy(x => x.Consumption)],// Less consumption
            _ => [] // Throw exception?
        };
    }
}