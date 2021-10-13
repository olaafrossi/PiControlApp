// Created: 2021|10|12
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|GpioDevice.cs|PiControlApp
// Olaaf Rossi

using System.Device.Gpio;
using Microsoft.Extensions.Logging;

namespace PiControlApp.ConsoleUI.Devices
{
    public class GpioDevice : IGpioDevice
    {
        private const int Pin = 18;
        private readonly GpioController _controller;
        private readonly ILogger<GpioDevice> _log;

        public GpioDevice(ILogger<GpioDevice> log)
        {
            GpioController controller = new();
            _controller = controller;
            _controller.OpenPin(Pin, PinMode.Output);
            _log = log;
            _log.LogInformation("GPIO Device has been created using pin # {Pin} in output mode", Pin);
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
                _log.LogInformation("Led On");
            }
            else
            {
                _controller.Write(Pin, PinValue.Low);
                _log.LogInformation("Led Off");
            }
        }
    }
}