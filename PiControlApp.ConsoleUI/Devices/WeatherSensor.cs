// Created: 2021|10|27
// Modified: 2021|10|27
// PiControlApp.ConsoleUI|WeatherSensor.cs|PiControlApp
// Olaaf Rossi

using System;
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
        private Bme280 _sensor;
        private string _units;

        public WeatherSensor(IConfiguration config, ILogger<WeatherSensor> log)
        {
            _config = config;
            _log = log;
            _log.LogInformation("Created the Weather Sensor");
            GetSetting();
        }

        public void ConnectToSensor()
        {
            try
            {
                I2cConnectionSettings i2CSettings = new(1, Bmx280Base.DefaultI2cAddress);
                I2cDevice i2CDevice = I2cDevice.Create(i2CSettings);
                Bme280 sensor = new(i2CDevice);
                _sensor = sensor;
                SensorStatusOk = true;
            }
            catch (Exception e)
            {
                _log.LogCritical("Cannot connect to sensor", e);
                SensorStatusOk = false;
            }
        }

        public WeatherReading ReadWeather()
        {
            Stopwatch timer = new();
            timer.Start();
            int measurementTime = _sensor.GetMeasurementDuration();

            _sensor.SetPowerMode(Bmx280PowerMode.Forced);
            Thread.Sleep(measurementTime);

            _sensor.TryReadTemperature(out Temperature tempValue);
            _sensor.TryReadAltitude(out Length altValue);
            _sensor.TryReadHumidity(out RelativeHumidity humValue);
            _sensor.TryReadPressure(out Pressure preValue);

            long time = timer.ElapsedMilliseconds;
            _log.LogInformation("Time for Weather Sensor Reading was {time} m/sec", time);
            timer.Stop();

            WeatherReading output = new()
            {
                Id = 1,
                Pressure = preValue.InchesOfMercury,
                Humidity = humValue.Percent,
                Altitude = altValue.Feet,
                Temperature = tempValue.DegreesFahrenheit,
                Units = _units
            };

            return output;
        }

        public bool SensorStatusOk { get; private set; }

        private void GetSetting()
        {
            string units = _config.GetValue<string>("Units");
            _units = units;
        }

        public void Dispose()
        {
            _sensor.Dispose();
        }
    }
}