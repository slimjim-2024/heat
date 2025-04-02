using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using HeatingOptimizer.SourceDataManager;

namespace HeatingOptimizer.UI;

public partial class StartWindow : Window
{
    private ViewModels.StartWindowViewModel startWindowViewModel = new();
    public StartWindow()
    {
        InitializeComponent();
        DataContext = startWindowViewModel;
    }
    private async void LoadMachines(object sender, RoutedEventArgs e)
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
                new FilePickerFileType("JSON Files(*.json)")
                {
                    Patterns=["*.json"],
                },
            },
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory), // Open the file dialog in the current directory
            AllowMultiple = false // Allow only one file to be selected
        });

        if (result != null && result.Count > 0)
        {
            // Get the path of the selected file
            var LocalPath = result[0].Path.AbsolutePath;
            startWindowViewModel.PathToMachines = LocalPath;
            if(LocalPath.Contains(".json"))
            {
                startWindowViewModel.productionUnits= DataParser.ParseMachineDataJson(LocalPath);
            }
            else if(LocalPath.Contains(".csv"))
            {
                startWindowViewModel.productionUnits = DataParser.ParseMachineDataCSV(LocalPath);
            }
            else
            {
                startWindowViewModel.PathToMachines = "The file is not a csv or json file";
                return;
            }
        }
        
    }
    private void ConfirmClick(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new(startWindowViewModel.productionUnits);
        mainWindow.Show();
        Close();
    }
}