// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 18
// by Olaaf Rossi

using System;
using System.Device.I2c;
using System.Text;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using UnitsNet;

namespace PiControlApp.ConsoleUI
{
    public class WeatherSensor
    {
        private readonly int _measurementTime;
        private readonly Bme280 _sensor;

        public WeatherSensor()
        {
            I2cConnectionSettings i2CSettings = new(1, Bmx280Base.DefaultI2cAddress);
            I2cDevice i2CDevice = I2cDevice.Create(i2CSettings);
            Bme280 sensor = new(i2CDevice);
            int measurementTime = sensor.GetMeasurementDuration();
            Console.WriteLine($"Sensor Measurement time is {measurementTime} ms");
            Thread.Sleep(500);

            _sensor = sensor;
            _measurementTime = measurementTime;
        }

        /// <summary>
        ///     Gets the Pressure- pass in "inches" to get inches of Mercury, otherwise you will get Hectopascals
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadPressure(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadPressure(out Pressure preValue);

            string output;
            if (string.Equals(units, "inches", StringComparison.OrdinalIgnoreCase))
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
            _sensor.TryReadHumidity(out RelativeHumidity humValue);
            string output = $"{humValue.Percent:#.##} %Humidity";
            return output;
        }

        /// <summary>
        ///     Gets the altitude- pass in "feet" to get Feet, otherwise you will get Metric
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadAltitude(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadAltitude(out Length altValue);

            string output;
            if (string.Equals(units, "feet", StringComparison.OrdinalIgnoreCase))
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
        ///     Gets the Temp- pass in "f" to get Fahrenheit otherwise you get Commie units
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public string ReadTemperature(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadTemperature(out Temperature tempValue);

            string output;
            if (string.Equals(units, "f", StringComparison.OrdinalIgnoreCase))
            {
                output = $"{tempValue.DegreesFahrenheit} F";
            }
            else
            {
                output = $"{tempValue.DegreesCelsius} C";
            }

            return output;
        }

        public string GetAllReadings(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadTemperature(out Temperature tempValue);
            _sensor.TryReadAltitude(out Length altValue);
            _sensor.TryReadHumidity(out RelativeHumidity humValue);
            _sensor.TryReadPressure(out Pressure preValue);
            StringBuilder sb = new();
            string output;
            if (string.Equals(units, "i", StringComparison.OrdinalIgnoreCase))
            {
                
                sb.Append($"{tempValue.DegreesFahrenheit} F");
                sb.AppendLine();
                sb.Append($"{altValue.Feet:#.##} Feet");
                sb.AppendLine();
                sb.Append($"{humValue.Percent:#.##} %Humidity");
                sb.AppendLine();
                sb.Append($"{preValue.InchesOfMercury} Inches");
                sb.AppendLine();
                output = sb.ToString();
            }
            else
            {
                sb.Append($"{tempValue.DegreesCelsius} C");
                sb.AppendLine();
                sb.Append($"{altValue.Meters:#.##} Meters");
                sb.AppendLine();
                sb.Append($"{humValue.Percent:#.##} %Humidity");
                sb.AppendLine();
                sb.Append($"{preValue.Hectopascals:#.##} Hectopascals");
                sb.AppendLine();
                output = sb.ToString();
            }

            return output;
        }
    }
}