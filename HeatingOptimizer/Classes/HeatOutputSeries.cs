using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace HeatingOptimizer;

public class HeatOutputSeries : StackedAreaSeries
{
    
    
    public override void GenerateGraph(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> timeFrames, in Dictionary<string, List<Result>> results)
    {
        base.GenerateGraph(selectedProductionUnits, timeFrames, in results);
        Series.Add(new StackedAreaSeries<double?>
        {
            Name = "Heat Required",
            Values = new ObservableCollection<double?>(timeFrames.Select(el=> el.HeatDemand == 0.0 ? null : (double?)el.RemainingHeat)),
            Fill = new SolidColorPaint{Color = SKColors.DarkRed, PathEffect = new DashEffect([5, 5], 1)}, 
            LineSmoothness = 0
        });
    }
}