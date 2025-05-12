using HeatingOptimizer;
using HeatingOptimizer.Optimizer;

namespace HeatingOptimizer.tests;

public class ProdUnitSorterTests
{
    [Fact]
    public void Sort_Scenario1Cheapest_ReturnCorrectList()
    {
        // Arrange
        List<ProductionUnit> inputList = [
            // ProductionUnit(string name, double maxHeatOutput, double maxElectricity, decimal productionCosts, int co2Emissions, double consumption)
        ];
        TimeFrame timeframe = new(DateTime.Now, DateTime.Now, 0, 0);
        short sortType = 0; // Cheapest
        var expectedList = inputList.OrderBy(x => x.ProductionCosts).ToList();

        // Act
        var sortedList = ProdUnitSorter.Sort(inputList, timeframe, sortType);

        // Assert
        Assert.Equal(sortedList, expectedList);
    }
}