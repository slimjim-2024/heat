namespace heat.tests;
using HeatingOptimizer;
using Xunit;

public class ProdUnitSorterTests
{
    [Fact]
    public void Sort_Scenario1Cheapest_ReturnCorrectList()
    {
        // Arrange
        List<ProductionUnit> inputList = [
            new ProductionUnit("", 1 /*maxHeatOutput*/, 0/*maxElectricity*/, 2/*productionCosts*/, 0, 0),
            new ProductionUnit("", 1 , 0, 3, 0, 0),
            new ProductionUnit("", 1 , 0, 4, 0, 0),
            new ProductionUnit("", 2 , 0, 4, 0, 0),
        ];
        decimal electricityPrice = 0;
        short sortType = 0; // Cheapest
        var expextedList = inputList.OrderBy(x => x.ProductionCosts).ToList();

        // Act
        var sortedList = ProdUnitSorter.Sort(inputList, electricityPrice, sortType);

        // Assert
        Assert.Equal(sortedList, expextedList);
    }
}