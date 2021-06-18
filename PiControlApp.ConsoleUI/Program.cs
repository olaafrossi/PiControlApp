// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 12
// by Olaaf Rossi
// scp -r C:\Dev\PiControlApp\PiControlApp.ConsoleUI\bin\Release\net5.0\publish\* pi@pi01:Desktop/Deployment/
using System;
using System.Device.Gpio;
using System.Threading;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;

namespace PiControlApp.ConsoleUI
{
    public class Program
    {

        private static void Main(string[] args)
        {
            var i2cSettings = new I2cConnectionSettings(1, Bme280.DefaultI2cAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);
            var bme280 = new Bme280(i2cDevice);

            int measurementTime = bme280.GetMeasurementDuration();
            bme280.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(measurementTime);

            //bme280.TryReadTemperature(out var tempValue);
            //bme280.TryReadPressure(out var preValue);
            //bme280.TryReadHumidity(out var humValue);
            //bme280.TryReadAltitude(out var altValue);

            //Console.WriteLine($"Temperature: {tempValue.DegreesCelsius:0.#}\u00B0C");
            //Console.WriteLine($"Pressure: {preValue.Hectopascals:#.##} hPa");
            //Console.WriteLine($"Relative humidity: {humValue.Percent:#.##}%");
            //Console.WriteLine($"Estimated altitude: {altValue.Meters:#} m");

            //string weather = $"temp {tempValue} + pressure {preValue} + humidity {humValue} + altitude {altValue}";

            Thread.Sleep(1000);

            Console.WriteLine("Hello World!");
            var signalRConnection = new SignalRConnection();
            signalRConnection.Start();
            //Thread.Sleep(100);
            signalRConnection.SendMessage("pi01", $"{DateTime.Now.Millisecond} initial startup");

            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
            int pin = 18;

            using var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
            bool ledOn = true;
            int count = 1;
            while (true)
            {

                bme280.TryReadTemperature(out var tempValue);
                bme280.TryReadPressure(out var preValue);
                bme280.TryReadHumidity(out var humValue);
                bme280.TryReadAltitude(out var altValue);
                string weather = $"temp {tempValue} + pressure {preValue} + humidity {humValue} + altitude {altValue}";
                weather = $"temp {tempValue.DegreesCelsius:0.#} + pressure {preValue.Hectopascals:#.##} + humidity {humValue.Percent:#.##} + altitude {altValue.Meters:#}";
                controller.Write(pin, ledOn ? PinValue.High : PinValue.Low);
                signalRConnection.SendMessage("pi01", $"{DateTime.Now.Millisecond} LED Blink");
                Thread.Sleep(200);
                signalRConnection.SendMessage("pi01weather", $"{weather}");
                signalRConnection.SendMessage("pi01counter", $"{count}");
                count++;
                //signalRConnection.SendInt("pi01", count);
                Thread.Sleep(1000);
                ledOn = !ledOn;
            }
        }
    }
}

