using Mottracker.Application.Services;
using Xunit;

namespace Mottracker.Tests.UnitTests;

public class MlPredictionServiceTests
{
    [Fact]
    public void PredictMotoDemand_ShouldReturnValidPrediction()
    {
        // Arrange
        var mlService = new MlPredictionService();
        var totalMotos = 50;
        var data = new DateTime(2024, 6, 15); // Um dia de semana

        // Act
        var prediction = mlService.PredictMotoDemand(totalMotos, data);

        // Assert
        Assert.True(prediction >= 0);
        Assert.True(prediction <= totalMotos);
    }

    [Fact]
    public void PredictMotoDemand_ShouldReturnDifferentValuesForDifferentDates()
    {
        // Arrange
        var mlService = new MlPredictionService();
        var totalMotos = 50;
        var weekday = new DateTime(2024, 6, 10); // Segunda-feira
        var weekend = new DateTime(2024, 6, 15); // Sábado

        // Act
        var weekdayPrediction = mlService.PredictMotoDemand(totalMotos, weekday);
        var weekendPrediction = mlService.PredictMotoDemand(totalMotos, weekend);

        // Assert
        Assert.True(weekdayPrediction >= 0);
        Assert.True(weekendPrediction >= 0);
        Assert.True(weekdayPrediction <= totalMotos);
        Assert.True(weekendPrediction <= totalMotos);
    }

    [Fact]
    public void PredictMotoDemand_ShouldHandleEdgeCases()
    {
        // Arrange
        var mlService = new MlPredictionService();
        var smallTotal = 1;
        var largeTotal = 1000;
        var data = DateTime.Now;

        // Act
        var smallPrediction = mlService.PredictMotoDemand(smallTotal, data);
        var largePrediction = mlService.PredictMotoDemand(largeTotal, data);

        // Assert
        Assert.True(smallPrediction >= 0 && smallPrediction <= smallTotal);
        Assert.True(largePrediction >= 0 && largePrediction <= largeTotal);
    }
}

