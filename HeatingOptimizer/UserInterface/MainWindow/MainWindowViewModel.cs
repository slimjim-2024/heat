using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using CommunityToolkit.Mvvm.ComponentModel;
using HeatingOptimizer.SourceDataManager;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Drawing;
using System;
using System.Linq;
using HeatingOptimizer.Optimizer;


namespace HeatingOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private Random rand = new Random();
        protected internal Dictionary<string, SKColor> colorDict = new()
        {
            {"GB1", SKColors.LimeGreen},
            {"GB2", SKColors.Yellow},
            {"OB1", SKColors.Crimson},
            {"GM1", SKColors.Cyan},
            {"HP1", SKColors.Orange}
        };

        [ObservableProperty]
        private List<string> _seasonSelection = ["Summer", "Winter"];
        protected internal Dictionary<string, Results> ResultDictionary = new Dictionary<string, Results>();
        [ObservableProperty]
        private string _inputText = string.Empty;

        [ObservableProperty]
        private string _selectedSeason = "Winter";


        [ObservableProperty]
        private ObservableCollection<ISeries> _series = [];

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieSeries = [];
        [ObservableProperty]
        private string _pointInfo = string.Empty;

        [ObservableProperty]
        private ObservableCollection<ICartesianAxis> _xAxes = [new Axis
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
        public ICartesianAxis[] YAxes { get; set; } =
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
        [RelayCommand]
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
                    Fill = new SolidColorPaint { Color = colorDict[item.Key] },
                    DataLabelsPaint = new SolidColorPaint { Color = SKColors.White },
                    DataLabelsSize = 20,
                });
            }
            // }
        }

        [RelayCommand]
        public void GenerateButton_Click(string sender)
        {
            if (sender.GetType() == typeof(string))
            {
                Console.WriteLine("Sender is a string, not a button.");
            }
            // Returns if no machine or heating data
            if (SelectedProductionUnits.Count == 0 || Frames is null) return;

            Series.Clear();
            CostCalculator.CalculateSeason(SelectedProductionUnits, Frames[sender],
            SelectedIndex, ref ResultDictionary);

            // Displays timeframes on x axis
            XAxes[0].Labels = [.. Frames[sender].Select(TF => TF.TimeFrom.ToString("dd/MM H:mm"))];
            XAxes[0].LabelsRotation = 90;
            XAxes[0].LabelsDensity = 0;
            XAxes[0].TextSize = 10;
            XAxes[0].MinStep = 1;

            Series.Add(
               new LineSeries<double> { Name = "Winter Heat Demand", Values = new ObservableCollection<double>(Frames[sender].Select(s => s.HeatDemand)), Fill = null, }
            );
            foreach (var PU in ResultDictionary)
            {
                Series.Add(new StackedAreaSeries<double> { Name = PU.Key, Values = PU.Value.HeatProduced, Fill = new SolidColorPaint { Color = colorDict[PU.Key] }, });
            }

            XAxes[0].MinLimit = 0;
            YAxes[0].MinLimit = 0;
        }
        protected internal Dictionary<string, List<TimeFrame>> Frames;

        [ObservableProperty]
        public ObservableCollection<ProductionUnit> _allProductionUnits;
        public List<ProductionUnit> SelectedProductionUnits { get; set; } = [];

        // checking which of the options is selected by their index

        [ObservableProperty]
       private short _selectedIndex = 0; // Default to first index

        public void Generate()
        {
            // Add way to get timeframe
            // CostCalculator.CalculatePeriod(SelectedProductionUnits, , SelectedIndex);
        }
        public MainWindowViewModel()
        {
        }
    }
}
