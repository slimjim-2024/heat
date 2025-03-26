using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Reactive;

namespace HeatingOptimizer.Optimizer
{
    static class CostCalculator
    {        
        private static void CalculateTimeframe(TimeFrame timeframe, List<ProductionUnit> prodUnits,
        ref Dictionary<string, Results> resultDict)
        {
            double remainingHeat = timeframe.HeatDemand;
            foreach (var prodUnit in prodUnits)
            {
                // Calculates pUnit's result data
                double heatProduced = Math.Min(remainingHeat, prodUnit.MaxHeatOutput);
                remainingHeat -= heatProduced;
                
                double fraction = heatProduced / prodUnit.MaxHeatOutput;
                double electricityProduced = fraction * prodUnit.MaxElectricity;
                
                decimal cost = (decimal)(heatProduced*(double)prodUnit.ProductionCosts - electricityProduced*(double)timeframe.ElectricityPrice);

                double emissions = heatProduced*prodUnit.CO2Emissions;

                double consumption = heatProduced*prodUnit.Consumption;

                // Saves results
                
            }
        }

        public static void CalculateSeason(List<ProductionUnit> prodUnits, List<TimeFrame> period, short sortType,
        out Dictionary<string, Results> resultDict)
        {
            resultDict = new Dictionary<string, Results>();
            // Makes prodUnit.SeasonHeatProduction empty before calculation
            // foreach (var prodUnit in prodUnits) prodUnit.SeasonHeatProduction.Clear();
            
            for (int i = 0; i < period.Count; i++) // Loop through each timeframe
            {
                // Sort prodUnits on first timeframe
                // Sort on every timeframe if looking for cheapest solution, as electicity prices change every timeframe
                if (i==1 || sortType==0 /*cheapest*/)
                    prodUnits = ProdUnitSorter.Sort(prodUnits, period[i], sortType);
                CalculateTimeframe(period[i], prodUnits, ref resultDict);
            }
        }
    }
}
