using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;

namespace HeatingOptimizer;

public class ProfitLossSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public string Name { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; }
    public ObservableCollection<ICartesianAxis> YAxes { get; set; }
    public void GenerateGraph(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> timeFrames,
        in Dictionary<string, List<Result>> results)
    {
        Series.Clear();
        YAxes[0].Name = Name;
   
        List<decimal> costs = new List<decimal>();
        for (var i = 0; i < timeFrames.Count; ++i)
        {
            decimal currentCost = 0;
            foreach (var result in results)
            {
                 currentCost -= result.Value[i].ProductionCosts;
            }
            costs.Add(currentCost);
        }
        
        Series.Add(new LineSeries<decimal>
        {
            Name = Name, 
            Values =new ObservableCollection<decimal>(costs)
        });
        Series.Add(new LineSeries<decimal>
        {
            Name = $"{Name} lost",
            Values = new ObservableCollection<decimal>(costs.Select(el => -el))
            
        });
        XAxes[0].MinLimit = 0;
        
    }
}