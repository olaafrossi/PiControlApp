using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace PiControlApp.ConsoleUI
{
    public class GpioDevice
    {
        private readonly GpioController _controller;
        const int pin = 18;

        public GpioDevice()
        {
            GpioController controller = new();
            _controller = controller;
            _controller.OpenPin(pin, PinMode.Output);
        }

        /// <summary>
        /// Turns on/off the LED connected to Pin 18
        /// </summary>
        /// <param name="state"></param>
        public void LedOn(bool state)
        {
            if (state is true)
            {
                _controller.Write(pin, PinValue.High);
            }
            else
            {
                _controller.Write(pin, PinValue.Low);
            }
        }
    }
}
