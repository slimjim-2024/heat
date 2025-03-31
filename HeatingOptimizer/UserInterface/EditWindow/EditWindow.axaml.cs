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
        public EditWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
        public EditWindow(ref MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _viewModel = new EditWindowViewModel(ref mainWindowViewModel); // Initialize the ViewModel with the production units
            DataContext = _viewModel;
        }
    }
}
