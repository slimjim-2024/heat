namespace HeatingOptimizer;

static class ProdUnitSorter
{
    /*
    public enum SortType
    {
        Cheapest,
        MoreGasEfficient(LessConsumption),
        LessCO2Emissions
    }
    */

    public static List<ProductionUnit> Sort(List<ProductionUnit> pUnits, Timeframe timeframe, short sortType)
    {
        return sortType switch
        {
            // (Heat cost - Electricity return)/Total heat production = cost/return per unit of heat
            0 => pUnits.OrderBy(x => (x.MaxHeatOutput * (double)x.ProductionCosts - x.MaxElectricity * (double)timeframe.ElectricityPrice) / x.MaxHeatOutput).ToList(),
            1 => pUnits.OrderBy(x => x.CO2Emissions).ToList(),// Less emissions
            2 => pUnits.OrderBy(x => x.Consumption).ToList(),// Less consumption
            _ => [] // Throw exception?
        };
    }
}