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
    public static List<ProductionUnit> Machines = new(); // list of machines

    public MachineParser(string path)
    {
        Machines = DataParser.ParseMachineDataCSV(path); // get the data from the csv file
    }

    public void ParseMachineData(string path, out List<ProductionUnit> machines) // function for getting data from csv file
    {
        string? line = null;
        machines = new List<ProductionUnit>(); // list of machines
        try
        {
            using (StreamReader sr = new(path)) //reads the .csv file
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