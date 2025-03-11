
namespace HeatingOptimizer
{
    class CostCalculator
    {
        public static void CalculateTimeframe(Timeframe timeframe, List<ProductionUnit> prodUnits)
        {
            double remainingHeat = timeframe.HeatDemand;
                foreach (var prodUnit in prodUnits)
                {
                    // Calculate heat
                    double heatProduced = Math.Min(remainingHeat, prodUnit.MaxHeatOutput);
                    prodUnit.SeasonHeatProduction.Append(heatProduced);
                    remainingHeat -= heatProduced;

                    // Calculate electricity
                    // Check w/ teacher if formulae is right
                    double fraction = heatProduced / prodUnit.MaxHeatOutput;
                    double electricityProduced = fraction * prodUnit.MaxElectricity;

                    // Calculate cost
                    prodUnit.SeasonProductionCosts.Append(
                        heatProduced*(double)prodUnit.ProductionCosts - electricityProduced*(double)timeframe.ElectricityPrice
                        );
                }
        }

        // prodUnits is an already sorted list of the machines in use
        public static void CalculateSeason(List<ProductionUnit> prodUnits, List<Timeframe> Season)
        {
            // Makes prodUnit.SeasonHeatProduction empty before calculation
            foreach (var prodUnit in prodUnits) prodUnit.SeasonHeatProduction = [];

            foreach (Timeframe timeframe in Season)
            {
                /* 
                When trying to get the cheapest solution,
                timeframes' electricity price should be taken into account:
                if (any prodUnit produces electricity && calculating cheapest solution)
                    Sort prodUnits;
                */
                CalculateTimeframe(timeframe, prodUnits);
            }
        }
    }
}
