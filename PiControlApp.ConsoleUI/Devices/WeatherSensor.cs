// Created: 2021|10|12
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|WeatherSensor.cs|PiControlApp
// Olaaf Rossi

using System.Device.I2c;
using System.Diagnostics;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PiControlApp.ConsoleUI.Models;
using UnitsNet;

namespace PiControlApp.ConsoleUI.Devices
{
    public class WeatherSensor : IWeatherSensor
    {
        private readonly IConfiguration _config;
        private readonly ILogger<WeatherSensor> _log;
        private readonly Bme280 _sensor;

        public WeatherSensor(IConfiguration config, ILogger<WeatherSensor> log)
        {
            I2cConnectionSettings i2CSettings = new(1, Bmx280Base.DefaultI2cAddress);
            I2cDevice i2CDevice = I2cDevice.Create(i2CSettings);
            Bme280 sensor = new(i2CDevice);
            _sensor = sensor;
            _config = config;
            _log = log;
            _log.LogInformation("Created the Weather Sensor");
        }

        /// <summary>
        /// Gets all of the sensor readings in Imperial Units
        /// </summary>
        /// <returns></returns>
        public WeatherReading GetAllReadingsImperial()
        {
            string units = _config.GetValue<string>("Units");
            Stopwatch timer = new();
            timer.Start();
            int measurementTime = _sensor.GetMeasurementDuration();
            _log.LogInformation("Weather Sensor Measurement Time is {measurementTime}", measurementTime);

            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(measurementTime);
            _sensor.TryReadTemperature(out Temperature tempValue);
            Thread.Sleep(measurementTime);
            _sensor.TryReadAltitude(out Length altValue);
            Thread.Sleep(measurementTime);
            _sensor.TryReadHumidity(out RelativeHumidity humValue);
            Thread.Sleep(measurementTime);
            _sensor.TryReadPressure(out Pressure preValue);

            long time = timer.ElapsedMilliseconds;
            _log.LogInformation("Time for Weather Sensor Reading was {time} m/sec", time);
            timer.Stop();

            WeatherReading output = new()
            {
                Pressure = preValue.InchesOfMercury,
                Humidity = humValue.Percent,
                Altitude = altValue.Feet,
                Temperature = tempValue.DegreesFahrenheit,
                Units = units
            };

            return output;
        }
    }
}