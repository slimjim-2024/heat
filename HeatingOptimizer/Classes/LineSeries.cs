using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;

namespace HeatingOptimizer;

public class LineSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public string Name { get; set; }
    public Func<TimeFrame, double> Selection { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; }
    public ObservableCollection<ICartesianAxis> YAxes { get; set; }
    public void GenerateGraph(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> timeFrames,
        in Dictionary<string, List<Result>> results)
    {
        Series.Clear();
        YAxes[0].Name = Name;

        Series.Add(new LineSeries<double>{Name = Name, 
            Values = new ObservableCollection<double>(timeFrames.Select(Selection))});
    }
}