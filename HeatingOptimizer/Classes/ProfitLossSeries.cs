using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatingOptimizer;

public class ProfitLossSeries : IViewableSeries
{
    public ObservableCollection<ISeries> Series { get; set; } = [];
    public string Name { get; set; }
    public ObservableCollection<ICartesianAxis> XAxes { get; set; } = [new Axis
    {
        Name = "Time Period",
        NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
        TextSize = 18,
        LabelsRotation = 90,
        LabelsDensity = -0.1f,
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
        // YAxes[0].MinLimit = 0;
        
    }
}