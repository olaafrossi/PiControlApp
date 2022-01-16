using System;
using System.Collections.Generic;
using System.Device.Gpio;
using Microsoft.Extensions.Logging;

// pin value low means it sees light

namespace PiControlApp.ConsoleUI.Devices
{
    public class LightSensor
    {
        private const int Pin = 17;
        private readonly GpioController _controller;
        private readonly ILogger<LightSensor> _log;

        public LightSensor(ILogger<LightSensor> log)
        {
            GpioController controller = new();
            _controller = controller;
            _controller.OpenPin(Pin, PinMode.Input);
            _log = log;
            _log.LogInformation("GPIO Device has been created using pin # {Pin} in input mode", Pin);
        }

        public int LightReading()
        {
            int output;
            PinValue status = _controller.Read(Pin);
            output = (int)status;

            return output;
        }
    }
}
