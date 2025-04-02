using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HeatingOptimizer.ViewModels;

public partial class StartWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _pathToMachines;

    protected internal List<ProductionUnit> productionUnits;

    public StartWindowViewModel()
    {

    }




}
