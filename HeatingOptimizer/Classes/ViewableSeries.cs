using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeatingOptimizer.Optimizer;
using HeatingOptimizer.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatingOptimizer
{

    public class ViewableSeries 
    {
        protected internal ObservableCollection<ISeries> Series {get; set;} = [];
        
        protected internal ObservableCollection<ISeries> PieSeries {get; set;} = [];
        protected internal Dictionary<string, Results> ResultDictionary = new();
        protected internal string Name {get; set;} = string.Empty;

        public ObservableCollection<ICartesianAxis> XAxes {get; set;} = [new Axis
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
        // Y Axis
        protected internal ICartesianAxis[] YAxes { get; set; } =
        [
        new Axis
        {
            Name = "Heat Demand",
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
            }
        }
        ];

        public void PressedCommand(PointerCommandArgs args)
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
        }
        public void GenerateGraph(string sender, List<ProductionUnit> SelectedProductionUnits,
        List<TimeFrame> Period, short SelectedIndex)
        {
            if (sender.GetType() == typeof(string))
            {
                Console.WriteLine("Sender is a string, not a button.");
            }
            // Returns if no machine or heating data
            if (SelectedProductionUnits.Count == 0 || Period is null) return;

            Series.Clear();
            CostCalculator.CalculateSeason(SelectedProductionUnits, Period,
            SelectedIndex, ref ResultDictionary);
            // Displays timeframes on x axis
            XAxes[0].Labels = [.. Period.Select(TF => TF.TimeFrom.ToString("dd/MM H:mm"))];
            XAxes[0].LabelsRotation = 90;
            XAxes[0].LabelsDensity = 0;
            XAxes[0].TextSize = 10;
            XAxes[0].MinStep = 1;

            Series.Add(
               new LineSeries<double> { Name = $"{sender} Heat Demand", Values = new ObservableCollection<double>(Period.Select(s => s.HeatDemand)), Fill = null, }
            );
            foreach (var PU in ResultDictionary)
            {
                Series.Add(new StackedAreaSeries<double> { Name = PU.Key, Values = PU.Value.HeatProduced, Fill = new SolidColorPaint { Color = MainWindowViewModel.colorDict[PU.Key] }, });
            }

            XAxes[0].MinLimit = 0;
            YAxes[0].MinLimit = 0;
        }
    }
}