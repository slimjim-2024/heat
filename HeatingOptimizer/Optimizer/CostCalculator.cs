using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Reactive;

namespace HeatingOptimizer.Optimizer
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

        public static void CalculatePeriod(List<ProductionUnit> prodUnits, List<Timeframe> period, short sortType)
        {
            // Makes prodUnit.SeasonHeatProduction empty before calculation
            foreach (var prodUnit in prodUnits) prodUnit.SeasonHeatProduction.Clear();
            
            for (int i = 0; i < period.Count; i++) // Loop through each timeframe
            {
                // Sort prodUnits on first timeframe
                // Sort on every timeframe if looking for cheapest solution, as electicity prices change every timeframe
                if (i==1 || sortType==0 /*cheapest*/)
                    prodUnits = ProdUnitSorter.Sort(prodUnits, period[i], sortType);
                CalculateTimeframe(period[i], prodUnits);
            }
        }
    }
}
