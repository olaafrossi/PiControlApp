// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 12
// by Olaaf Rossi
// scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@pi01:Desktop/Deployment/
// https://endjin.com/blog/2019/09/passwordless-ssh-from-windows-10-to-raspberry-pi

using System;
using System.Threading;

namespace PiControlApp.ConsoleUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            WeatherSensor sensor = new();
            GpioDevice led = new();
            
            //SignalRConnection signalRConnection = new SignalRConnection();

            //signalRConnection.Start();
            //Thread.Sleep(100);
            //signalRConnection.SendMessage("pi01", $"{DateTime.Now.Millisecond} initial startup");

            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");

            int count = 1;

            while (true)
            {
                led.LedOn(true);
                Thread.Sleep(10);
                led.LedOn(false);
                Console.WriteLine(sensor.ReadPressure("inches"));
                Console.WriteLine(sensor.ReadPressure("hecto"));
                Console.WriteLine(sensor.ReadHumidity());
                Console.WriteLine(sensor.ReadAltitude("feet"));
                Console.WriteLine(sensor.ReadAltitude("meters"));
                Console.WriteLine(sensor.ReadTemperature("f"));
                Console.WriteLine(sensor.ReadTemperature("c"));
                count++;
                Console.WriteLine($"number of samples {count}");
            }
        }
    }
}