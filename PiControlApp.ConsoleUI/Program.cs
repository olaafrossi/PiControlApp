// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 12
// by Olaaf Rossi
// scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@pi01:Desktop/Deployment/

using System;
using System.Device.Gpio;
using System.Threading;

namespace PiControlApp.ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var signalRConnection = new SignalRConnection();
            signalRConnection.Start("Pi01", "Initial Connection");

            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
            int pin = 18;

            using var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
            bool ledOn = true;
            while (true)
            {
                controller.Write(pin, ledOn ? PinValue.High : PinValue.Low);
                signalRConnection.Start("Pi01", "Led Blink");
                Thread.Sleep(1000);
                ledOn = !ledOn;
            }
        }
    }
}