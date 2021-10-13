// Created: 2021|10|12
// Modified: 2021|10|12
// PiControlApp.ConsoleUI|IGpioDevice.cs|PiControlApp
// Olaaf Rossi

namespace PiControlApp.ConsoleUI
{
    public interface IGpioDevice
    {
        /// <summary>
        ///     Turns on/off the LED connected to Pin 18
        /// </summary>
        /// <param name="state"></param>
        void LedState(bool state);
    }
}