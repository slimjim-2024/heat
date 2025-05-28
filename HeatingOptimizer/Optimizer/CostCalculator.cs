using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace HeatingOptimizer.Optimizer
{
    public static class CostCalculator
    {        
        private static double CalculateTimeframe(TimeFrame timeframe, List<ProductionUnit> prodUnits,
        ref Dictionary<string, List<Result>> resultDict)
        {
            double remainingHeat = timeframe.HeatDemand;
            foreach (var prodUnit in prodUnits)
            {
                // Calculates pUnit's result data
                double heatProduced = Math.Min(remainingHeat, prodUnit.MaxHeatOutput);
                remainingHeat -= heatProduced;
                
                double fraction = heatProduced / prodUnit.MaxHeatOutput;
                double electricityProduced = fraction * prodUnit.MaxElectricity;
                
                decimal cost = (decimal)(heatProduced*(double)prodUnit.ProductionCosts);
                decimal revenue = (decimal)(electricityProduced*(double)timeframe.ElectricityPrice);

                double emissions = heatProduced*prodUnit.CO2Emissions;

                double consumption = heatProduced*prodUnit.Consumption;

                // Saves results
                resultDict[prodUnit.Name].Add(new Result(heatProduced, electricityProduced, cost, revenue,
                    emissions, consumption, timeframe.TimeFrom));
            }
            return remainingHeat;
        }

        public static void CalculateSeason(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> period, short sortType,
        ref Dictionary<string, List<Result>> resultDict)
        {
            // Sorts data and prepares dictionary
            selectedProductionUnits = ProdUnitSorter.Sort(selectedProductionUnits, period[0], sortType);
            resultDict = new Dictionary<string, List<Result>>();
            foreach (var productionUnit in selectedProductionUnits)
            {
                resultDict[productionUnit.Name] = new List<Result>();
            }
            
            // Loop through each timeframe
            foreach (var timeFrame in period)
            {
                // Sort on every timeframe if looking for cheapest solution, as electricity prices change every timeframe
                if (sortType==0)
                    selectedProductionUnits = ProdUnitSorter.Sort(selectedProductionUnits, timeFrame, sortType);
                double remainingHeat = CalculateTimeframe(timeFrame, selectedProductionUnits, ref resultDict);
                timeFrame.RemainingHeat = remainingHeat;
            }
        }
    }
}
