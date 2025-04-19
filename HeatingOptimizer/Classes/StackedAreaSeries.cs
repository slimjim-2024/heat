using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeatingOptimizer.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatingOptimizer;

public class StackedAreaSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public string Name { get; set; }
    public double MinLimit { get; set; } = 0;
    public Func<Result, double> Selection { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; } = [new Axis
    {
        Name = "Time Period",
        NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
        TextSize = 18,
        LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
        SeparatorsPaint = new SolidColorPaint
        {
            Color = SKColor.Parse("#ffffff"),
            StrokeThickness = 1,
        },
        ZeroPaint = new SolidColorPaint
        {
            Color = SKColor.Parse("#808080"),
            StrokeThickness = 2
        },
        TicksPaint = new SolidColorPaint
        {
            Color = SKColor.Parse("#B0B0B0"),
            StrokeThickness = 1.5f
        },
        MinLimit=0,
    }];
            
    public ObservableCollection<ICartesianAxis> YAxes { get; set; } =
    [
        new Axis
        {
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#ffffff"),
                StrokeThickness = 1,
            },
            ZeroPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#000"),
                StrokeThickness = 2
            },
            TicksPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
                StrokeThickness = 1.5f
            },
        }
    ];

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
        YAxes[0].MinLimit = MinLimit;
        
    }
}