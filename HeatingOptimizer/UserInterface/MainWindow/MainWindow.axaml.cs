using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using System;
using HeatingOptimizer.ViewModels;
using Avalonia.Interactivity;
using HeatingOptimizer.SourceDataManager;
using HeatingOptimizer.Optimizer;
using System.Collections.ObjectModel;



namespace HeatingOptimizer.UI;

public partial class MainWindow : Window
{
    private MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
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
            mainWindowViewModel.InputText=LocalPath.Replace("%20", " ");
            DataParser.ParseHeatingDataCSV(mainWindowViewModel.InputText, ref mainWindowViewModel.Frames);
        }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
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
}