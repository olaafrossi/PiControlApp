// Created: 2021|09|29
// Modified: 2021|10|12
// PiControlApp.ConsoleUI|GpioDevice.cs|PiControlApp
// Olaaf Rossi

using System.Device.Gpio;

namespace PiControlApp.ConsoleUI
{
    public class GpioDevice : IGpioDevice
    {
        private const int Pin = 18;
        private readonly GpioController _controller;

        public GpioDevice()
        {
            GpioController controller = new();
            _controller = controller;
            _controller.OpenPin(Pin, PinMode.Output);
        }

        /// <summary>
        ///     Turns on/off the LED connected to Pin 18
        /// </summary>
        /// <param name="state"></param>
        public void LedState(bool state)
        {
            if (state is true)
            {
                _controller.Write(Pin, PinValue.High);
            }
            else
            {
                _controller.Write(Pin, PinValue.Low);
            }
        }
    }
}