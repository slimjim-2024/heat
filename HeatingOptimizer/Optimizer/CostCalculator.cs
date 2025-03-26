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
                resultDict[prodUnit.Name].HeatProduced.Add(heatProduced);
                resultDict[prodUnit.Name].ElectricityProduced.Add(electricityProduced);
                resultDict[prodUnit.Name].ProductionCosts.Add(cost);
                resultDict[prodUnit.Name].CO2Emissions.Add(emissions);
                resultDict[prodUnit.Name].Consumption.Add(consumption);
            }
        }

        public static void CalculateSeason(List<ProductionUnit> prodUnits, List<TimeFrame> period, short sortType,
        ref Dictionary<string, Results> resultDict)
        {
            // Sorts data and prepares dictonary
            prodUnits = ProdUnitSorter.Sort(prodUnits, period[0], sortType);
            resultDict = new Dictionary<string, Results>();
            foreach (var i in prodUnits)
            {
                resultDict[i.Name] = new Results();
            }
            
            // Loop through each timeframe
            for (int i = 0; i < period.Count; i++)
            {
                // Sort on every timeframe if looking for cheapest solution, as electicity prices change every timeframe
                if (sortType==0)
                    prodUnits = ProdUnitSorter.Sort(prodUnits, period[i], sortType);
                CalculateTimeframe(period[i], prodUnits, ref resultDict);
            }
        }
    }
}
