using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeatingOptimizer.Optimizer;
using HeatingOptimizer.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatingOptimizer
{

    public interface IViewableSeries 
    {
        protected internal ObservableCollection<ISeries> Series {get; set;} 
        
        // protected internal ObservableCollection<ISeries> PieSeries {get; set;} = [];
        protected internal string Name {get; set;}
        protected internal Func<TimeFrame, double> Selection { get; set; }

        public ObservableCollection<ICartesianAxis> XAxes {get; set;}
        // Y Axis
        protected internal ObservableCollection<ICartesianAxis> YAxes { get; set; }

        /*public void PressedCommand(PointerCommandArgs args)
        {
            var foundPoints = args.Chart.GetPointsAt(args.PointerPosition);
            PieSeries.Clear();
            // foreach (var point in foundPoints)
            // {
            var point = foundPoints.FirstOrDefault();
            if (point == null) return;

            var resultValues = ResultDictionary.Where(s => s.Value.HeatProduced[point.Index] > 0).ToList();
            foreach (var item in resultValues)
            {
                PieSeries.Add(new PieSeries<double>()
                {
                    Name = item.Key,
                    Values = [double.Round(item.Value.HeatProduced[point.Index], 2, MidpointRounding.AwayFromZero)],
                    Fill = new SolidColorPaint { Color = MainWindowViewModel.colorDict[item.Key] },
                    DataLabelsPaint = new SolidColorPaint { Color = SKColors.White },
                    DataLabelsSize = 20,
                });
            }
            // }
        }*/
        void GenerateGraph(List<ProductionUnit> selectedProductionUnits, in List<TimeFrame> period, in Dictionary<string, Results> results);
        /*{
            /*if (sender.GetType() is string)
            {
                Console.WriteLine("Sender is a string, not a button.");
            }#1#
            // Returns if no machine or heating data
            if (selectedProductionUnits.Count == 0) return;

            Series.Clear();
            CostCalculator.CalculateSeason(selectedProductionUnits, period,
            selectedIndex, ref ResultDictionary);
            // Displays timeframes on X axis
            XAxes[0].Labels = [.. period.Select(tf => tf.TimeFrom.ToString("dd/MM H:mm"))];
            XAxes[0].LabelsRotation = 90;
            XAxes[0].LabelsDensity = 0;
            XAxes[0].TextSize = 10;
            XAxes[0].MinStep = 1;

            Series.Add(
               new LineSeries<double> { Name = $"{sender} Heat Demand", Values = new ObservableCollection<double>(period.Select(Selection)), Fill = null}
            );
            foreach (var pu in ResultDictionary)
            {
                Series.Add(new StackedAreaSeries<double> { Name = pu.Key, Values = pu.Value.HeatProduced, Fill = new SolidColorPaint { Color = MainWindowViewModel.colorDict[pu.Key] }, });
            }

            XAxes[0].MinLimit = 0;
            YAxes[0].MinLimit = 0;
        }*/
    }
}
