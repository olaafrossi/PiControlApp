// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 12
// by Olaaf Rossi
// scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@pi01:Desktop/Deployment/
// https://endjin.com/blog/2019/09/passwordless-ssh-from-windows-10-to-raspberry-pi

using System;
using System.Threading;
using JetBrains.Annotations;

namespace PiControlApp.ConsoleUI
{
    [UsedImplicitly]
    public class Program
    {

        private static void Main(string[] args)
        {
            string url = "http://192.168.1.138:5000/SensorHub";
            int count = 1;

            Console.WriteLine("Hello World!");
            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");

            WeatherSensor sensor = new();
            GpioDevice led = new();
            SignalRConnection signalRConnection = new(url);

            signalRConnection.SendMessageAsync("pi01", $"{DateTime.Now.Millisecond} initial startup");
            signalRConnection.SendIntAsync("pi01-Counter", count);

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

                if (count % 8 is not 0)
                {
                    signalRConnection.SendMessageAsync("pi01", $"{count} count % 8 is not 0");
                }
                if (count % 2 is not 1)
                {
                    signalRConnection.SendIntAsync("pi01-Counter", count);
                }
            }
        }
    }
}