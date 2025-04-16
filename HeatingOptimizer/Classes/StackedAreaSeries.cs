using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;

namespace HeatingOptimizer;

public class StackedAreaSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public Dictionary<string, Results> ResultDictionary { get; set; }
    public string Name { get; set; }
    public Func<TimeFrame, double> Selection { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; }
    public ObservableCollection<ICartesianAxis> YAxes { get; set; }

    public void GenerateGraph(List<ProductionUnit> selectedProductionUnits, in List<TimeFrame> period, in Dictionary<string, Results> results)
    {
        foreach (var selectedProductionUnit in selectedProductionUnits)
        {
            Series.Add(new StackedAreaSeries<double>{Name = selectedProductionUnit.Name, 
                Values = results[selectedProductionUnit.Name].HeatProduced});
        }
    }
}