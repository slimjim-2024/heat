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

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ViewModel(); // Set the DataContext here
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