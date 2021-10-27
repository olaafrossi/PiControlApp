// Created: 2021|10|23
// Modified: 2021|10|27
// PiControlApp.Tests|MonitorServiceTests.cs|PiControlApp
// Olaaf Rossi

using Moq;
using PiControlApp.ConsoleUI;
using Xunit;

namespace PiControlApp.Tests
{
    public class MonitorServiceTests
    {
        [Fact]
        public void IncrementFailedSensorCount_ShouldIncrement()
        {
            Mock<MonitorService> service = new();

            // Arrange
            int expected = 1;

            //Act


            //service.Setup(i => i.IncrementFailedSensorCount(1)).;

            //var actual = service.Setup(mock => mock.IncrementFailedSensorCount(1));
            //var act2 = service.Setup(mock => mock.FailedSensorRead);
            //int actual = 1;

            // Assert
            //Assert.Equal(expected, service);
        }
    }
}