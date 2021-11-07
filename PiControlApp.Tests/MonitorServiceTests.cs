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
            var mock = new Mock<IMonitorService>();
            mock.Setup(foo => foo.RunLoop).Returns(false);
            mock.Setup(foo => foo.Run()).Returns(true);

            //var instance = new MonitorService();

            // Arrange
            //int expected = 0;

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