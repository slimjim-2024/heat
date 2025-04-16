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
                resultDict[prodUnit.Name].Times.Add(timeframe.TimeFrom);
            }
        }

        public static void CalculateSeason(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> period, short sortType,
        ref Dictionary<string, Results> resultDict)
        {
            // Sorts data and prepares dictionary
            selectedProductionUnits = ProdUnitSorter.Sort(selectedProductionUnits, period[0], sortType);
            resultDict = new Dictionary<string, Results>();
            foreach (var productionUnit in selectedProductionUnits)
            {
                resultDict[productionUnit.Name] = new Results();
            }
            
            // Loop through each timeframe
            foreach (var timeFrame in period)
            {
                // Sort on every timeframe if looking for cheapest solution, as electricity prices change every timeframe
                if (sortType==0)
                    selectedProductionUnits = ProdUnitSorter.Sort(selectedProductionUnits, timeFrame, sortType);
                CalculateTimeframe(timeFrame, selectedProductionUnits, ref resultDict);
            }
        }
    }
}
