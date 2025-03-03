// Note for the csv file
//  [0] is time from,  [1] is time to, [2] is heat demand, [3] is an Electricity price
using System;
using System.Globalization;

namespace HeatingOptimizer;
class HeatingData
{

    protected internal static List<Timeframe> WinterTimeFrame = new List<Timeframe>(); // list of timeframes
    protected internal static List<Timeframe> SummerTimeFrame = new List<Timeframe>(); // list of timeframes
    private static string? line;
    public static void GetData(string path = "heating_data.csv") // function for getting data from csv file
    {
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
    public static List<ProductionUnit> GetProductionUnits(string fileName)
    {
        List<ProductionUnit> productionUnits = new List<ProductionUnit>();
        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string? line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(",");
                    productionUnits.Add(new ProductionUnit(data[0], double.Parse(data[1], CultureInfo.InvariantCulture), double.Parse(data[2], CultureInfo.InvariantCulture), decimal.Parse(data[3], CultureInfo.InvariantCulture), int.Parse(data[4]), double.Parse(data[5], CultureInfo.InvariantCulture)));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return productionUnits;
    }
}