using System;
using PiControlApp.ConsoleUI.Devices;
using Xunit;
using Moq;
using Moq.Language.Flow;
using PiControlApp.ConsoleUI;

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
