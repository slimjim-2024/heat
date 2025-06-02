using HeatingOptimizer.Optimizer;
using NUnit.Framework;
using System.Reflection;

namespace HeatingOptimizer.Tests
{
    [TestFixture]
    public class CostCalculatorTests
    {
        private TimeFrame CreateTimeFrame(double heatDemand, double electricityPrice)
        {
            return new TimeFrame(DateTime.Now, DateTime.Now.AddHours(1), heatDemand, (decimal)electricityPrice);
        }

        private ProductionUnit CreateProductionUnit(string name, double maxHeat, double maxElectricity, decimal cost)
        {
            return new ProductionUnit
            {
                Name = name,
                MaxHeatOutput = maxHeat,
                MaxElectricity = maxElectricity,
                ProductionCosts = cost
            };
        }

        private void CallCalculate(TimeFrame tf, List<ProductionUnit> units, Dictionary<string, List<Result>> results)
        {
            typeof(CostCalculator)
                .GetMethod("CalculateTimeframe", BindingFlags.NonPublic | BindingFlags.Static)!
                .Invoke(null, new object[] { tf, units, results });
        }

        [Test]
        public void Test_Normal_Calculation()
        {
            var tf = CreateTimeFrame(100, 0.2);
            var unit = CreateProductionUnit("Boiler", 120, 50, 0.1m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results.ContainsKey("Boiler"), Is.True);
        }

        [Test]
        public void Test_Empty_Units()
        {
            var tf = CreateTimeFrame(100, 0.2);
            var results = new Dictionary<string, List<Result>>();

            CallCalculate(tf, new List<ProductionUnit>(), results);

            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Zero_HeatDemand()
        {
            var tf = CreateTimeFrame(0, 0.2);
            var unit = CreateProductionUnit("Boiler", 100, 50, 0.1m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["Boiler"][0].HeatProduced, Is.EqualTo(0));
        }

        [Test]
        public void Test_Negative_HeatDemand()
        {
            var tf = CreateTimeFrame(-50, 0.2);
            var unit = CreateProductionUnit("Boiler", 100, 50, 0.1m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            Assert.DoesNotThrow(() => CallCalculate(tf, new List<ProductionUnit> { unit }, results));
        }

        [Test]
        public void Test_Excess_Production_Capacity()
        {
            var tf = CreateTimeFrame(50, 0.2);
            var unit = CreateProductionUnit("Boiler", 100, 20, 0.1m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["Boiler"][0].HeatProduced, Is.EqualTo(50));
        }

        [Test]
        public void Test_High_Production_Cost()
        {
            var tf = CreateTimeFrame(100, 0.1);
            var unit = CreateProductionUnit("ExpensiveUnit", 100, 0, 5.0m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["ExpensiveUnit"][0].ProductionCosts, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Zero_Electricity_Price()
        {
            var tf = CreateTimeFrame(100, 0.0);
            var unit = CreateProductionUnit("ZeroPriceUnit", 100, 100, 1.0m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["ZeroPriceUnit"][0].ElectricityProduced, Is.GreaterThan(0));
        }

        [Test]
        public void Test_Fractional_Production()
        {
            var tf = CreateTimeFrame(50, 1.0);
            var unit = CreateProductionUnit("PartialUnit", 100, 100, 0.5m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["PartialUnit"][0].HeatProduced, Is.EqualTo(50));
        }

        [Test]
        public void Test_Maxed_Unit_Output()
        {
            var tf = CreateTimeFrame(150, 0.3);
            var unit = CreateProductionUnit("Maxed", 100, 20, 0.2m);
            var results = new Dictionary<string, List<Result>>
            {
                { unit.Name, new List<Result>() }
            };

            CallCalculate(tf, new List<ProductionUnit> { unit }, results);

            Assert.That(results["Maxed"][0].HeatProduced, Is.EqualTo(100));
        }

        [Test]
        public void Test_Multiple_Units()
        {
            var tf = CreateTimeFrame(150, 0.3);
            var unit1 = CreateProductionUnit("Unit1", 80, 30, 0.1m);
            var unit2 = CreateProductionUnit("Unit2", 100, 40, 0.2m);
            var units = new List<ProductionUnit> { unit1, unit2 };
            var results = new Dictionary<string, List<Result>>
            {
                { unit1.Name, new List<Result>() },
                { unit2.Name, new List<Result>() }
            };

            CallCalculate(tf, units, results);

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results["Unit1"][0].HeatProduced + results["Unit2"][0].HeatProduced, Is.EqualTo(150).Within(0.01));
        }
    }
}
