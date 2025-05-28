using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Kernel.Events;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HeatingOptimizer.Optimizer;
using LiveChartsCore.Kernel.Sketches;


namespace HeatingOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        protected internal static Dictionary<string, SKColor> colorDict = new()
        {
            {"GB1", SKColors.LimeGreen},
            {"GB2", SKColors.Yellow},
            {"OB1", SKColors.Coral},
            {"GM1", SKColors.Cyan},
            {"HP1", SKColors.Teal}
        };
        protected internal Dictionary<string, List<TimeFrame>> Frames;

        [ObservableProperty] private string _titleText = "Heating Optimizer";

        [ObservableProperty] private int _gridColumns = 2;

        [ObservableProperty] private ObservableCollection<IViewableSeries> _selectedGraph = [];

        [ObservableProperty]
        private ObservableCollection<ProductionUnit> _allProductionUnits;
        public List<ProductionUnit> SelectedProductionUnits { get; set; } = [];

        // checking which of the options is selected by their index

        [ObservableProperty]
        private short _selectedIndex = 0; // Default to first index


        [ObservableProperty]
        private List<string> _seasonSelection = ["Summer", "Winter"];
        protected internal Dictionary<string, List<Result>> ResultsDict = new();

        // protected internal Dictionary<string, List<Result>> ResultDictionary = new();
        [ObservableProperty]
        private string _inputText = string.Empty;

        [ObservableProperty]
        private string _selectedSeason = "Winter";

        [ObservableProperty] private bool _isPaneOpen = false;

        [RelayCommand] protected internal void PaneInteractionCommand() => IsPaneOpen = !IsPaneOpen;

        [ObservableProperty]
        private ObservableCollection<IViewableSeries> _allSeries = [
            new StackedAreaSeries{Name="Heat generated per machine" ,Selection = s=> s.HeatProduced,
                },
            new LineSeries{Name=" Electricity price", Selection = s=> (double)s.ElectricityPrice,
                },
            new ProfitLossSeries{Name = "Money spent", },
            new StackedAreaSeries{Name = "Electricity generated", Selection = s=> s.ElectricityProduced,
                MinLimit = -6},
            new LineSeries{Name = "Heat Demand", Selection = s=> s.HeatDemand},
        ];
        [ObservableProperty] private ObservableCollection<IViewableSeries> _selectedSeries = [];

        /*
        [ObservableProperty] private ObservableCollection<ISeries> _series = [];
        */

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieSeries = [];
        [ObservableProperty]
        private string _pointInfo = string.Empty;

        [RelayCommand]
        private static void OpenNewWindow(IViewableSeries series)
        {
            Console.WriteLine(series);
        }

        // After loading data from file
        [RelayCommand]
        public async Task PrepareLoadedData()
        {
            await Task.Run(() =>
            {
                List<TimeFrame> timeFrames = [];

                // Results are already loaded

                // Clears series that now countain outdated data
                foreach (var series in AllSeries)
                {
                    series.Series.Clear();
                }

                GenerateGraphs();
            });
        }

        // When  Timeframe or Machine Data  or  Selected Season or Machines  change
        [RelayCommand]
        public async Task PrepareCalculatedData()
        {
            await Task.Run(() =>
            {
                // If any data is missing, returns
                if (SelectedProductionUnits.Count == 0 || Frames is null || Frames.Count == 0) return;

                List<TimeFrame> timeFrames = Frames[SelectedSeason];

                // Calculates results
                CostCalculatorV2.CalculateSeason(SelectedProductionUnits, Frames[SelectedSeason],
                    SelectedIndex, ref ResultsDict);

                // Clears series that now countain outdated data
                foreach (var series in AllSeries)
                {
                    series.Series.Clear();
                }

                GenerateGraphs();
            });
        }

        // After preparing data and When selected graphs change
        public async void GenerateGraphs()
        {
            await Task.Run(() =>
            {
                if (SelectedSeries.Count == 0 || ResultsDict.Count == 0)
                {
                    return;
                }
                
                List <TimeFrame> timeFrames = Frames[SelectedSeason];
                // Gets labels from the first result's TimeFrom list
                List<string> labels = [.. ResultsDict.First().Value.Select(result => result.TimeFrom.ToString("dd/MM H:mm"))];

                foreach (var series in SelectedSeries)
                {
                    // If data has not yet been generated for that graph
                    if (series.Series.Count == 0)
                    {
                        // Gets labels from the first result's TimeFrom list
                        series.XAxes[0].Labels = labels;
                        series.XAxes[0].LabelsRotation = 90;
                        series.XAxes[0].LabelsDensity = -0.1f;
                        series.XAxes[0].TextSize = 9;
                        series.XAxes[0].MinStep = 1;

                        series.GenerateGraph(SelectedProductionUnits, timeFrames, in ResultsDict);
                    }
                }
            });
        }
    }
}
