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
                Thread.Sleep(1);
                led.LedOn(false);
                string pressureImperial = sensor.ReadPressure("inches");
                string pressureMetric = sensor.ReadPressure("hecto");
                string humidity = sensor.ReadHumidity();
                string altitudeImperial = sensor.ReadAltitude("feet");
                string altitudeMetric = sensor.ReadAltitude("meters");
                string temperatureImperial = sensor.ReadTemperature("f");
                string temperatureMetric = sensor.ReadTemperature("c");

                // TODO create a new method in the WeatherSensor class to get all values

                StringBuilder sb = new();
                sb.Append(pressureImperial);
                sb.AppendLine();
                sb.Append(pressureMetric);
                sb.AppendLine();
                sb.Append(humidity);
                sb.AppendLine();
                sb.Append(altitudeImperial);
                sb.AppendLine();
                sb.Append(altitudeMetric);
                sb.AppendLine();
                sb.Append(temperatureImperial);
                sb.AppendLine();
                sb.Append(temperatureMetric);
                sb.AppendLine();
                sb.Append(DateTime.Now.ToString("hh:mm:ss.FFF"));
                string message = sb.ToString();

                Console.WriteLine(message);
                signalRConnection.SendMessageAsync("pi01-Data", message);

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