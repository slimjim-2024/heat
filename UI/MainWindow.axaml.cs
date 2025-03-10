using Avalonia.Controls;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using CommunityToolkit.Mvvm.ComponentModel;
// using Avalonia.Controls.Charts;
// using Avalonia.Controls.Charts.Series;

namespace HeatingOptimizer;
public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    private static ISeries[] _series = [];
    public ViewModel()
    {
        Series = new ISeries[]
        {
            new StackedAreaSeries<double?>{Values=[ 3, 2, 3, 3, 2, 4, 9 ], LineSmoothness = 0},
            new StackedAreaSeries<double?>{Values= [6, 5, 6, 0, 0, 5, 2 ], LineSmoothness = 0},
            new StackedAreaSeries<double?>{Values=[ 4, 8, 2, 8, 9, 0, 0], LineSmoothness = 0},
        };
    }

    // X Axis
    public ICartesianAxis[] XAxes { get; set; } = new ICartesianAxis[]
    {
        new Axis
        {
            Name = "X Axis",
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
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
            }
        }
    };

    // Y Axis
    public ICartesianAxis[] YAxes { get; set; } = new ICartesianAxis[]
    {
        new Axis
        {
            Name = "Y Axis",
            NamePaint = new SolidColorPaint(SKColor.Parse("#808080")),
            TextSize = 18,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#B0B0B0")),
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColor.Parse("#B0B0B0"),
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
            }
        }
    };
}


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void BrowseFile()
    {
        var dialog = new OpenFileDialog();
        dialog.AllowMultiple = false;
        dialog.Directory = ".";
        dialog.Filters = new List<FileDialogFilter>
        {
            new FileDialogFilter { Name = "CSV Files", Extensions = new List<string> { "csv" } }
        };
        var result = dialog.ShowAsync(this);
        // return result.Result.FirstOrDefault();
    }
}