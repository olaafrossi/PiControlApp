// Created: 2021|10|21
// Modified: 2021|10|27
// PiControlApp.Tests|GpioDeviceTests.cs|PiControlApp
// Olaaf Rossi

using Moq;
using Moq.Language.Flow;
using PiControlApp.ConsoleUI.Devices;
using Xunit;

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