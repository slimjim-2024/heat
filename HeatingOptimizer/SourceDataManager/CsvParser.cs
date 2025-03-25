using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace HeatingOptimizer.SourceDataManager;

public class CsvParser
{
    protected internal static List<Timeframe> WinterTimeFrame = []; // list of timeframes
    protected internal static List<Timeframe> SummerTimeFrame = []; // list of timeframes
    protected internal static List<ProductionUnit> ProductionUnits = [];

    public static void ParseMachineData(string path = "machines.csv")
    {
        // Our class property names match our CSV file header names, we can read the file without any configuration.
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            ProductionUnits = csv.GetRecords<ProductionUnit>().ToList();
        }
    }

    public static void ParseHeatingData(string path = "heating_data.csv") // function for getting data from csv file
    {
        string? line = null;

        try
        {
            using (StreamReader sr = new StreamReader(path)) //reads the .csv file
            {
                // skip the first 3 lines
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();

                while ((line = sr.ReadLine()) != null) // if file is not empty, then return the data
                {
                    string[] periods = line.Split(",,"); // period from the csv file
                    string[] winter = periods[0].Split(","); // columns inside of the period in csv for WINTER
                    string[] summer = periods[1].Split(","); // columns inside of the period in csv for SUMMER
                    WinterTimeFrame.Add(new Timeframe(DateTime.Parse(winter[0]), DateTime.Parse(winter[1]), Convert.ToDouble(winter[2], CultureInfo.InvariantCulture), Convert.ToDecimal(winter[3], CultureInfo.InvariantCulture))); // adds the data to the list
                    SummerTimeFrame.Add(new Timeframe(DateTime.Parse(summer[0]), DateTime.Parse(summer[1]), Convert.ToDouble(summer[2], CultureInfo.InvariantCulture), Convert.ToDecimal(summer[3], CultureInfo.InvariantCulture))); // adds the data to the list
                }
            }
        }
        catch (Exception e) // if exception appears, then return a message that file could not be read. 
        {
            Console.WriteLine($"The line was {line ?? "null"}");
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}