using Avalonia.Controls;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using Avalonia.Diagnostics.Screenshots;
using Avalonia.Platform.Storage;
using System;
using System.Diagnostics;
using HeatingOptimizer.ViewModels;
using Avalonia.Interactivity;
// using Avalonia.Controls.Charts;
// using Avalonia.Controls.Charts.Series;

namespace HeatingOptimizer.UI;

public partial class MainWindow : Window
{
    private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
    public MainWindow()
    {

        InitializeComponent();
        DataContext = mainWindowViewModel; // Set the DataContext here
    }
    public async void BrowseFile( object sender, RoutedEventArgs e )
    {
        IReadOnlyList<IStorageFile> result = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open a csv file with the timeFrames",
            // File type picker
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("CSV Files(*.csv)")
                {
                    Patterns = ["*.csv"]
                },
            },
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory), // Open the file dialog in the current directory
            AllowMultiple = false // Allow only one file to be selected
        });

        if (result != null && result.Count > 0) 
        {
            // Get the path of the selected file
            var LocalPath = result[0].Path.AbsolutePath;
            // Set the title of the window to the name of the file, removes extensions from name
            // Load the file and replacing "%20" with spaces, determines whether the file is in binary depending on the extension
            Console.WriteLine(LocalPath.Replace("%20", " "));
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e){
        Console.WriteLine($"{mainWindowViewModel.InputText}");
    }
}