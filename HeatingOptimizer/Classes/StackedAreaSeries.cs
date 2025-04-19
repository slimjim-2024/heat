using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeatingOptimizer.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;

namespace HeatingOptimizer;

public class StackedAreaSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public string Name { get; set; }
    public Func<Result, double> Selection { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; }
    public ObservableCollection<ICartesianAxis> YAxes { get; set; }

    public void GenerateGraph(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> timeFrames,
        in Dictionary<string, List<Result>> results)
    {
        Series.Clear();
        YAxes[0].Name = Name;

        foreach (var selectedProductionUnit in results)
        {
            Series.Add(new StackedAreaSeries<double>{Name = selectedProductionUnit.Key, 
                Values = new ObservableCollection<double>(selectedProductionUnit.Value.Select(Selection)),
                Fill = new SolidColorPaint{Color = MainWindowViewModel.colorDict[selectedProductionUnit.Key]}});
        }
        
    }
}