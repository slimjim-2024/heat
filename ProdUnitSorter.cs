namespace HeatingOptimizer;

static class ProductionUnitSorter
{
    public static List<ProductionUnit> Sort(List<ProductionUnit> pUnits, Timeframe tf, int sortType)
    {
        switch (sortType)
        {
            case 0:
            return pUnits.OrderBy(x => x.CO2Emissions).ToList(); // Less emissions

            case 1:
            return pUnits.OrderBy(x => x.Consumption).ToList(); // Less consumption

            case 2:
            // (Heat cost - Electricity return)/Total heat production = cost per unit of heat
            return pUnits.OrderBy(x => (x.MaxHeatOutput*(double)x.ProductionCosts - x.MaxElectricity*(double)tf.ElectricityPrice)/x.MaxHeatOutput).ToList(); // Cheapest

            // Not sure if this is something
            // return pUnits.OrderByDescending(x => (double)x.ProductionCosts/x.Consumption).ToList(); // More efficient

            default:
            return []; // Shouldn't happen
        } 
    }
}