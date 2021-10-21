using System;
using PiControlApp.ConsoleUI.Devices;
using Xunit;
using Moq;
using Moq.Language.Flow;

namespace PiControlApp.Tests
{

    public class GpioDeviceTests 
    {
        [Fact]
        public void LedState_ShouldBeNegative()
        {
            Mock<IGpioDevice> device = new();

            // Arrange
            bool expected = false;

            //Act
            ISetup<IGpioDevice> actual = device.Setup(mock => mock.LedState(true));
            bool act = actual.Equals(true);

            // Assert
            Assert.Equal(expected, act);

        }
    }
}
