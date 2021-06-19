// Created by Three Byte Intermedia, Inc.
// project: PiControlApp
// Created: 2021 06 18
// by Olaaf Rossi

using System.Device.I2c;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;

namespace PiControlApp.ConsoleUI
{
    public class WeatherSensor
    {
        private readonly int _measurementTime;
        private readonly Bme280 _sensor;

        public WeatherSensor()
        {
            I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, Bmx280Base.DefaultI2cAddress);
            I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);
            Bme280 sensor = new Bme280(i2cDevice);
            int measurementTime = sensor.GetMeasurementDuration();

            _sensor = sensor;
            _measurementTime = measurementTime;
        }

        public string ReadPressure(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadPressure(out var preValue);

            string output;
            if (units == "inches")
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

        public string ReadAltitude(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadAltitude(out var altValue);

            string output;
            if (units == "feet")
            {
                output = $"{altValue.Feet:#.##} Feet";
            }
            else
            {
                output = $"{altValue.Meters:#.##} Meters";
            }

            return output;
        }

        public string ReadTemperature(string units)
        {
            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(_measurementTime);
            _sensor.TryReadTemperature(out var tempValue);

            string output;
            if (units == "f")
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
