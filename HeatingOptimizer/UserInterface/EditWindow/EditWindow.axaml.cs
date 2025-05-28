using Avalonia.Controls;
using HeatingOptimizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace HeatingOptimizer.UI
{
    public partial class EditWindow : Window
    {
        private EditWindowViewModel _viewModel; // ViewModel for the EditWindow
        private MainWindowViewModel _mainWindowViewModel;
        public EditWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
        public EditWindow(ref MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _viewModel = new EditWindowViewModel(ref mainWindowViewModel); // Initialize the ViewModel with the production units
            DataContext = _viewModel;
        }

        private async void DataGrid_OnCellEditEnded(object? sender, DataGridCellEditEndedEventArgs e)
        {
            await _mainWindowViewModel.PrepareCalculatedData();
        }
    }
}
