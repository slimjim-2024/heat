using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace HeatingOptimizer;

public enum ProductionUnitType
{
    GasBoiler,
    OilBoiler,
    GasMotor,
    HeatPump
}

public class ProductionUnit
{
    public string Name { get; set; } = "";
    public double MaxHeatOutput { get; set; }
    public double MaxElectricity { get; set; }
    public decimal ProductionCosts { get; set; }
    public int CO2Emissions { get; set; }
    public double Consumption { get; set; }

    // public Bitmap _imageLocation; // Image for the production unit
    
    // [NotMapped]
    // public Bitmap ImageLocation
    // {
    //     get
    //     {
    //         if (_imageLocation == null)
    //         {
    //             // Load the image from the resources
    //             _imageLocation = new Bitmap("avares:/HeatingOptimizer/Assets/Images/" + Name + ".jpg");
    //         }
    //         return _imageLocation;
    //     }
    //     set
    //     {
    //         _imageLocation = value;
    //     }
    // }

    // // setting up the production unit
    public ProductionUnit() { }
}

