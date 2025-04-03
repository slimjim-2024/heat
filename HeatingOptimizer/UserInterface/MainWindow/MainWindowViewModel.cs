using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using CommunityToolkit.Mvvm.ComponentModel;
using HeatingOptimizer.SourceDataManager;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HeatingOptimizer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        protected internal Dictionary<string, Results> ResultDictionary = new Dictionary<string, Results>();
        [ObservableProperty]
        private string _inputText= string.Empty;

        [ObservableProperty]
        private ObservableCollection<ISeries> _series = [];

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
        protected internal List<TimeFrame> Frames;

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
        public ICartesianAxis[] YAxes { get; set; } =
        [
        new Axis
        {
            Name = "Efficiency",
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
    }
}
