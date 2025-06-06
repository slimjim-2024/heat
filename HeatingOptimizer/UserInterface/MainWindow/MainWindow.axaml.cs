using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using System;
using HeatingOptimizer.ViewModels;
using Avalonia.Interactivity;
using HeatingOptimizer.SourceDataManager;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using System.IO.Pipelines;
using System.IO;
using System.Text.Json;


namespace HeatingOptimizer.UI;

public partial class MainWindow : Window
{
    private MainWindowViewModel mainWindowViewModel = new();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = mainWindowViewModel; // Set the DataContext here
    }

    public MainWindow(List<ProductionUnit> units) : this()
    {
        mainWindowViewModel.AllProductionUnits = new ObservableCollection<ProductionUnit>(units);
        
    }

    public async void BrowseFile(object sender, RoutedEventArgs e)
    {
        IReadOnlyList<IStorageFile> result = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open a csv file with the timeframes",
            // File type picker
            FileTypeFilter = new List<FilePickerFileType>
            {
                new("CSV Files(*.csv)")
                {
                    Patterns = ["*.csv"]
                },
            },
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory), // Open the file dialog in the current directory
            AllowMultiple = false // Allow only one file to be selected
        });

        if (result.Count > 0)
        {
            // Get the path of the selected file
            var localPath = result[0].Path.AbsolutePath;
            // Set the title of the window to the path of the file
            mainWindowViewModel.InputText = localPath.Replace("%20", " ");

            DataParser.ParseHeatingDataCSV(mainWindowViewModel.InputText, ref mainWindowViewModel.Frames);

            await mainWindowViewModel.PrepareCalculatedData();
        }
    }

    private async void LoadButton_Click(object sender, RoutedEventArgs e)
    {
        var file = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Results file",
            FileTypeFilter =
            [
                new("JSON Files(*.json)")
                {
                    Patterns = ["*.json"]
                },
            ],
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory),
            SuggestedFileName = "results.json",
            AllowMultiple = false
        });

        if (file.Count > 0)
        {
            try
            {
                // Opens reading stream from the first file
                await using var stream = await file[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);

                // Asyncrounously deserializes JSON data
                // Updates results and prepares graphs
                mainWindowViewModel.ResultsDict = await JsonSerializer.DeserializeAsync<Dictionary<string, List<Result>>>(stream)??[];
                await mainWindowViewModel.PrepareLoadedData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading file: {ex.Message}");
            }
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (mainWindowViewModel.ResultsDict.Count == 0)
        {
            return;
        }

        var result = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Results file",
            FileTypeChoices = new List<FilePickerFileType>
            {
                new("JSON Files(*.json)")
                {
                    Patterns = ["*.json"]
                },
            },
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory),
            SuggestedFileName = "results.json",
            ShowOverwritePrompt = true
        });

        if (result != null)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            await using var stream = await result.OpenWriteAsync();
            await JsonSerializer.SerializeAsync(stream, mainWindowViewModel.ResultsDict, options);
        }
    }

    private void EditFile(object sender, RoutedEventArgs e)
    {
        // Create an instance of EditWindow
        var editWindow = new EditWindow(ref mainWindowViewModel);

        // Show the EditWindow
        editWindow.Show();
    }

    private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Clicked");
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        mainWindowViewModel.GridColumns =
            mainWindowViewModel.SelectedGraph.Count > 0 ? mainWindowViewModel.SelectedSeries.Count : 2;
        mainWindowViewModel.GridMaxHeight = mainWindowViewModel.SelectedGraph.Count > 0 ? 100 : 2000;
    }

    private void SelectedGraphs_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        mainWindowViewModel.GridColumns =
            mainWindowViewModel.SelectedGraph.Count > 0 ? mainWindowViewModel.SelectedSeries.Count : 2;
        mainWindowViewModel.GenerateGraphs();
    }

    private async void General_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        await mainWindowViewModel.PrepareCalculatedData();
    }
}