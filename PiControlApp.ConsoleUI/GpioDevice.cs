// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 19
// by Olaaf Rossi

using System.Device.Gpio;

namespace PiControlApp.ConsoleUI
{
    public class GpioDevice
    {
        private const int pin = 18;
        private readonly GpioController _controller;

        public GpioDevice()
        {
            GpioController controller = new();
            _controller = controller;
            _controller.OpenPin(pin, PinMode.Output);
        }

        /// <summary>
        ///     Turns on/off the LED connected to Pin 18
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