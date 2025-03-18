
using Avalonia.Reactive;

namespace HeatingOptimizer
{
    class CostCalculator
    {
        private static void CalculateTimeframe(Timeframe timeframe, List<ProductionUnit> prodUnits)
        {
            double remainingHeat = timeframe.HeatDemand;
            foreach (var prodUnit in prodUnits)
            {
                // Calculate heat
                double heatProduced = Math.Min(remainingHeat, prodUnit.MaxHeatOutput);
                remainingHeat -= heatProduced;
                
                // Calculate electricity // Check w/ supervisor if formulae is right
                double fraction = heatProduced / prodUnit.MaxHeatOutput;
                double electricityProduced = fraction * prodUnit.MaxElectricity;

                // Calculate cost
                decimal cost = (decimal)(heatProduced*(double)prodUnit.ProductionCosts - electricityProduced*(double)timeframe.ElectricityPrice);

                // Also record Gas Consumption?

                // Process results
                prodUnit.SeasonHeatProduction.Append(heatProduced);
                prodUnit.SeasonElectricityProduction.Append(electricityProduced);
                prodUnit.SeasonProductionCosts.Append(cost);
            }
        }

        public static void CalculateSeason(List<ProductionUnit> prodUnits, List<Timeframe> season, short sortType)
        {
            // Makes prodUnit.SeasonHeatProduction empty before calculation
            foreach (var prodUnit in prodUnits) prodUnit.SeasonHeatProduction = [];
            
            for (int i = 0; i < season.Count; i++) // Loop through each timeframe
            {
                /*
                Sort prodUnits on first timeframe
                ||
                Sort on every timeframe if looking for cheapest solution,
                as electicity prices change every timeframe
                */
                if (i==1 || sortType==0 /*cheapest*/)
                    prodUnits = ProdUnitSorter.Sort(prodUnits, season[i].ElectricityPrice, sortType);
                CalculateTimeframe(season[i], prodUnits);
            }
        }
    }
}
