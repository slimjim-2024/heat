using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;

namespace HeatingOptimizer.SourceDataManager;

public class DataParser
{
    protected internal static List<TimeFrame> WinterTimeFrame = []; // list of timeframes
    protected internal static List<TimeFrame> SummerTimeFrame = []; // list of timeframes

    public static List<ProductionUnit> ParseMachineDataCSV(string path = "machines.csv")
    {
        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            try
            {
                return [.. csv.GetRecords<ProductionUnit>()];
            }
            catch (Exception)
            {
                return [];
            }

            // Our class property names match our CSV file header names, we can read the file without any configuration.

        }
    }

    public static List<ProductionUnit> ParseMachineDataJson(string path = "machines.json")
    => JsonSerializer.Deserialize<List<ProductionUnit>>(File.ReadAllText(path)) ?? new List<ProductionUnit>();

    public static void ParseHeatingDataCSV(string path, ref Dictionary<string, List<TimeFrame>> timeFrames) // function for getting data from csv file
    {
        string? line = null;
        timeFrames = new(); // list of timeframes
        // if (File.Exists(path))return;
        try
        {
            using (StreamReader sr = new StreamReader(path)) //reads the .csv file
            {
                // skip the first 3 lines
                // sr.ReadLine();
                // sr.ReadLine();
                // sr.ReadLine();

                while ((line = sr.ReadLine()) != null) // if file is not empty, then return the data
                {
                    string[] periods = line.Split(",,"); // period from the csv file
                    string[] winter = periods[0].Split(","); // columns inside of the period in csv for WINTER
                    string[] summer = periods[1].Split(","); // columns inside of the period in csv for SUMMER

                    DateTime dateTimeFrom;
                    DateTime dateTimeTo;
                    double heatDemand;
                    decimal electricityPrice;
                    if (DateTime.TryParse(winter[0], CultureInfo.InvariantCulture, out dateTimeFrom) &&
                        DateTime.TryParse(winter[1], CultureInfo.InvariantCulture, out dateTimeTo) &&
                        double.TryParse(winter[2], CultureInfo.InvariantCulture, out heatDemand) &&
                        decimal.TryParse(winter[3], CultureInfo.InvariantCulture, out electricityPrice))
                        WinterTimeFrame.Add(new TimeFrame(dateTimeFrom, dateTimeTo, heatDemand, electricityPrice));
                    if (DateTime.TryParse(summer[0], CultureInfo.InvariantCulture, out dateTimeFrom) &&
                        DateTime.TryParse(summer[1], CultureInfo.InvariantCulture, out dateTimeTo) &&
                        double.TryParse(summer[2], CultureInfo.InvariantCulture, out heatDemand) &&
                        decimal.TryParse(summer[3], CultureInfo.InvariantCulture, out electricityPrice))
                        SummerTimeFrame.Add(new TimeFrame(dateTimeFrom, dateTimeTo, heatDemand, electricityPrice));


                    // WinterTimeFrame.Add(new Timeframe(DateTime.Parse(winter[0]), DateTime.Parse(winter[1]), Convert.ToDouble(winter[2], CultureInfo.InvariantCulture), Convert.ToDecimal(winter[3], CultureInfo.InvariantCulture))); // adds the data to the list
                    // SummerTimeFrame.Add(new Timeframe(DateTime.Parse(summer[0]), DateTime.Parse(summer[1]), Convert.ToDouble(summer[2], CultureInfo.InvariantCulture), Convert.ToDecimal(summer[3], CultureInfo.InvariantCulture))); // adds the data to the list
                }

                timeFrames.Add("Winter", WinterTimeFrame); // adds the data to the list
                timeFrames.Add("Summer", SummerTimeFrame); // adds the data to the list
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