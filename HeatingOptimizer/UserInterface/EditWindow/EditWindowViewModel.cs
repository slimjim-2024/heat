using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using HeatingOptimizer.SourceDataManager;
using System.Collections.Generic;
using HeatingOptimizer.ViewModels;

namespace HeatingOptimizer.UI
{
    public partial class EditWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProductionUnit> _machines;
        public EditWindowViewModel()
        {
        }
        public EditWindowViewModel(ref MainWindowViewModel mainWindowViewModel)
        {
            _machines = new ObservableCollection<ProductionUnit>(mainWindowViewModel.AllProductionUnits); // Initialize the collection with the provided production units
        }
            
    }
}