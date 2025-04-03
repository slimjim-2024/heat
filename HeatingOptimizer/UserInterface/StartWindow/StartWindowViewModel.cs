using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HeatingOptimizer.ViewModels;

public partial class StartWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _pathToMachines = "No file selected";

    protected internal List<ProductionUnit> productionUnits = [];


    



}
