using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using System;
using HeatingOptimizer.ViewModels;
using Avalonia.Interactivity;
using HeatingOptimizer.SourceDataManager;
using System.Collections.ObjectModel;
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
    
    public MainWindow(List<ProductionUnit> units): this()
    {
        mainWindowViewModel.AllProductionUnits=new ObservableCollection<ProductionUnit>(units);
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

        if (result != null && result.Count > 0)
        {
            // Get the path of the selected file
            var localPath = result[0].Path.AbsolutePath;
            // Set the title of the window to the path of the file
            mainWindowViewModel.InputText=localPath.Replace("%20", " ");
            
            DataParser.ParseHeatingDataCSV(mainWindowViewModel.InputText, ref mainWindowViewModel.Frames);
        }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
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
            SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Environment.CurrentDirectory), // Open the file dialog in the current directory
            SuggestedFileName = "",
            ShowOverwritePrompt = true // Show a prompt if the file already exists
        });

        if (result != null)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(mainWindowViewModel.ResultsDict, options);
            System.IO.File.WriteAllText(result.Path.AbsolutePath, json);
        }
    }

    private void EditFile(object sender, RoutedEventArgs e)
    {
        // Create an instance of EditWindow
        var editWindow = new EditWindow(ref mainWindowViewModel);

        // Show the EditWindow
        editWindow.Show();
    }
    private void AddData(object sender, RoutedEventArgs e)
    {
        // Create an instance of AddWindow
        var addWindow = new AddDataWindow();

        // Show the AddWindow
        addWindow.Show();
    }

    private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Clicked");
    }
}