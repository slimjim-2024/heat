namespace HeatingOptimizer;
class Program
{
    static void Main(string[] args)
    {
       HeatingData.GetData();
       foreach(var item in HeatingData.WinterTimeFrame)
       {
           Console.WriteLine($"Time from: {item.TimeFrom} Time to: {item.TimeTo} Heat demand: {item.HeatDemand} Electricity price: {item.ElectricityPrice}");
       }
       foreach(var item in HeatingData.SummerTimeFrame)
       {
           Console.WriteLine($"Summer Time from: {item.TimeFrom.TimeOfDay} Time to: {item.TimeTo.TimeOfDay} Heat demand: {item.HeatDemand} Electricity price: {item.ElectricityPrice}");
       }
    }
}