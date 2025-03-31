using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Controls.Shapes;
using CsvHelper;
using CsvHelper.Configuration;

namespace HeatingOptimizer.SourceDataManager;

public class MachineParser
{
    public string path = "machines.csv"; // path to the csv file
    
    protected internal static List<ProductionUnit> Machines = new List<ProductionUnit>(); // list of machines
    List<ProductionUnit> MachineData = DataParser.ParseMachineData(); // get the data from the csv file

    public MachineParser(string path)
    {
        Machines = DataParser.ParseMachineData(path); // get the data from the csv file
    }

    public static List<ProductionUnit> GetMachines()
    {
        return Machines; // return the list of machines
    }

    public static void SetMachines(List<ProductionUnit> machines)
    {
        Machines = machines; // set the list of machines
    }

    public void ParseMachineData(string path, out List<ProductionUnit> machines) // function for getting data from csv file
    {
        string? line = null;
        machines = new List<ProductionUnit>(); // list of machines
        try
        {
            using (StreamReader sr = new StreamReader(path)) //reads the .csv file
            {
                while ((line = sr.ReadLine()) != null) // if file is not empty, then return the data
                {
                    string[] MachineData = line.Split(",");
                    var productionUnit = new ProductionUnit
                    {
                        Name = MachineData[0], // name of the machine
                        MaxHeatOutput = double.Parse(MachineData[1], CultureInfo.InvariantCulture), // max heat output of the machine
                        MaxElectricity = double.Parse(MachineData[2], CultureInfo.InvariantCulture), // max electricity of the machine
                        ProductionCosts = decimal.Parse(MachineData[3], CultureInfo.InvariantCulture), // production costs of the machine
                        CO2Emissions = int.Parse(MachineData[4], CultureInfo.InvariantCulture), // CO2 emissions of the machine
                        Consumption = int.Parse(MachineData[5], CultureInfo.InvariantCulture) // consumption of the machine
                    }; // create a new production unit

                    machines.Add(productionUnit); // add the machine to the list of machines
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

}