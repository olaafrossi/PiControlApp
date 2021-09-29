// Created: 2021|09|29
// Modified: 2021|09|29
// PiControlApp.ConsoleUI|Program.cs|PiControlApp
// Olaaf Rossi

using System;
using System.Threading;
using JetBrains.Annotations;

namespace PiControlApp.ConsoleUI
{
    [UsedImplicitly]
    public class Program
    {
        // scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@cm01:Desktop/Deployment/
        // https://endjin.com/blog/2019/09/passwordless-ssh-from-windows-10-to-raspberry-pi
        // scp id_ed25519.pub pi@cm01:~\.ssh\authorized_keys
        // https://docs.microsoft.com/en-us/visualstudio/debugger/remote-debugging-dotnet-core-linux-with-ssh?view=vs-2019

        private static void Main(string[] args)
        {
            string url = "http://192.168.1.130:5000/SensorHub";
            int count = 1;

            Console.WriteLine("Hello World!");
            //Console.WriteLine("Blinking LED. Press Ctrl+C to end.");

            WeatherSensor sensor = new();
            GpioDevice led = new();
            //SignalRConnection signalRConnection = new(url);

            //signalRConnection.SendMessageAsync("pi01", $"{DateTime.Now.Millisecond} initial startup");
            //signalRConnection.SendIntAsync("pi01-Counter", count);

            while (true)
            {
                //Console.WriteLine("Blinking LED True");
                led.LedOn(true);
                Thread.Sleep(200);
                //Console.WriteLine("Blinking LED False");
                led.LedOn(false);
                Thread.Sleep(200);

                string weatherFreedom = sensor.GetAllReadings("i");
                string weatherCommunist = sensor.GetAllReadings("c");

                //Console.WriteLine($"Freedom Units {weatherFreedom} {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fffffff")}");
                //Console.WriteLine($"Communist Units {weatherCommunist}");
                Console.WriteLine(sensor.ReadPressure("inches"));
                Console.WriteLine(sensor.ReadHumidity());
                Console.WriteLine(sensor.ReadAltitude("feet"));
                Console.WriteLine(sensor.ReadTemperature("f"));


                //signalRConnection.SendMessageAsync("pi01-Data", weatherFreedom);
                //signalRConnection.SendMessageAsync("pi01-Data", weatherCommunist);

                count++;
                Console.WriteLine($"number of samples {count}");
                Thread.Sleep(1000);

                //if (count % 2 is not 0)
                //{
                //    signalRConnection.SendMessageAsync("pi01", $"{count} count % 2 is not 0");
                //}

                //if (count % 2 is not 1)
                //{
                //    signalRConnection.SendIntAsync("pi01-Counter", count);
                //}
            }
        }
    }
}