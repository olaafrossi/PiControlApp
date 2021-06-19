// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 18
// by Olaaf Rossi

using System;
using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Bmxx80.PowerMode;

namespace PiControlApp.ConsoleUI
{
    public class WeatherSensor
    {
        private readonly int _measurementTime;
        private readonly Bme280 _sensor;

        public WeatherSensor()
        {
            I2cConnectionSettings i2cSettings = new(1, Bmx280Base.DefaultI2cAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);
            Bme280 sensor = new(i2cDevice);
            int measurementTime = sensor.GetMeasurementDuration();
            Console.WriteLine($"Sensor Measurement time is {measurementTime} ms");
            Thread.Sleep(500);

            _sensor = sensor;
            _measurementTime = measurementTime;
        }

        /// <summary>
        /// Gets the Pressure- pass in "inches" to get inches of Mercury, otherwise you will get Hectopascals
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadPressure(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadPressure(out var preValue);

            string output;
            if (string.Equals(units, "inches", System.StringComparison.OrdinalIgnoreCase))
            {
                output = $"{preValue.InchesOfMercury} Inches";
            }
            else
            {
                output = $"{preValue.Hectopascals:#.##} Hectopascals";
            }

            return output;
        }

        public string ReadHumidity()
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadHumidity(out var humValue);
            string output = $"{humValue.Percent:#.##} %Humidity";
            return output;
        }

        /// <summary>
        /// Gets the altitude- pass in "feet" to get Feet, otherwise you will get Metric
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadAltitude(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadAltitude(out var altValue);

            string output;
            if (string.Equals(units, "feet", System.StringComparison.OrdinalIgnoreCase))
            {
                output = $"{altValue.Feet:#.##} Feet";
            }
            else
            {
                output = $"{altValue.Meters:#.##} Meters";
            }

            return output;
        }

        /// <summary>
        /// Gets the Temp- pass in "f" to get Fahrenheit otherwise you get Commie units
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadTemperature(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadTemperature(out var tempValue);

            string output;
            if (string.Equals(units, "f", System.StringComparison.OrdinalIgnoreCase))
            {
                output = $"{tempValue.DegreesFahrenheit} F";
            }
            else
            {
                output = $"{tempValue.DegreesCelsius} C";
            }

            return output;
        }
    }
}
