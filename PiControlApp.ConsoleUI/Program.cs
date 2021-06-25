// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 12
// by Olaaf Rossi

using System;
using System.Text;
using System.Threading;
using JetBrains.Annotations;

namespace PiControlApp.ConsoleUI
{
    [UsedImplicitly]
    public class Program
    {
        // scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@pi01:Desktop/Deployment/
        // https://endjin.com/blog/2019/09/passwordless-ssh-from-windows-10-to-raspberry-pi

        private static void Main(string[] args)
        {
            string url = "http://192.168.1.130:5000/SensorHub";
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
                Thread.Sleep(1);
                led.LedOn(false);

                string weatherFreedom = sensor.GetAllReadings("i");
                string weatherCommunist = sensor.GetAllReadings("c");

                Console.WriteLine($"Freedom Units {weatherFreedom} {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fffffff")}");
                Console.WriteLine($"Communist Units {weatherCommunist}");

                signalRConnection.SendMessageAsync("pi01-Data", weatherFreedom);
                //signalRConnection.SendMessageAsync("pi01-Data", weatherCommunist);

                count++;
                Console.WriteLine($"number of samples {count}");

                if (count % 2 is not 0)
                {
                    signalRConnection.SendMessageAsync("pi01", $"{count} count % 2 is not 0");
                }

                if (count % 2 is not 1)
                {
                    signalRConnection.SendIntAsync("pi01-Counter", count);
                }
            }
        }
    }
}