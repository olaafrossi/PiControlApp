// Created: 2021|10|12
// Modified: 2021|10|27
// PiControlApp.ConsoleUI|MonitorService.cs|PiControlApp
// Olaaf Rossi

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PiControlApp.ConsoleUI.DataAccess;
using PiControlApp.ConsoleUI.Devices;
using PiControlApp.ConsoleUI.Models;

[assembly: InternalsVisibleTo("PiControlApp.Tests")]

namespace PiControlApp.ConsoleUI

{
    public class MonitorService : IMonitorService
    {
        private readonly IConfiguration _config;
        private readonly IGpioDevice _led;
        private readonly ILogger<MonitorService> _log;
        private readonly IWeatherSensor _sensor;
        private readonly IWeatherData _server;
        private string _deviceId;
        private int _ledBlinkInterval;
        private string _units;
        private int _weatherSensorReadInterval;

        public MonitorService(ILogger<MonitorService> log, IConfiguration config, IWeatherSensor sensor, IGpioDevice led, IWeatherData server)
        {
            _log = log;
            _config = config;
            _sensor = sensor;
            _led = led;
            _server = server;
            GetSettings();
        }

        public void Run()
        {
            _log.LogInformation("starting the run loop");

            _log.LogInformation("the units set are {_units} the deviceID is {_deviceId} the read interval is {_weatherSensorReadInterval}", _units, _deviceId, _weatherSensorReadInterval);

            while (RunLoop is true)
            {
                LedBlink();

                WeatherReading reading = GetWeatherData();

                if (reading is not null)
                {
                    SendWeatherData(reading);
                    _log.LogInformation("Pressure is {reading.Pressure} Humidity is {reading.Humidity} Altitude is {reading.Altitude} Temperature is {reading.Temperature} Units are {reading.Units}", reading.Pressure, reading.Humidity, reading.Altitude, reading.Temperature, reading.Units);
                }
                else
                {
                    IncrementFailedSensorCount(1);
                    _log.LogError("Could not read from Weather Sensor");
                    Console.WriteLine(FailedSensorReadCount);
                }

                _log.LogInformation("Sleeping {_weatherSensorReadInterval}", _weatherSensorReadInterval);
                Thread.Sleep(_weatherSensorReadInterval);
            }
        }

        public int FailedSensorReadCount { get; private set; }

        public bool RunLoop { get; set; }

        private void LedBlink()
        {
            int ledBlinkInterval = _config.GetValue<int>("LedBlinkInterval");
            _led.LedState(true);
            Thread.Sleep(ledBlinkInterval);
            _led.LedState(false);
            Thread.Sleep(ledBlinkInterval);
        }

        private WeatherReading GetWeatherData()
        {
            WeatherReading output = new();
            _sensor.ConnectToSensor();

            if (_sensor.DeviceStatusOk is true)
            {
                output = _sensor.ReadFromSensor();
            }
            else if (_sensor.DeviceStatusOk is false)
            {
                _log.LogCritical("Device Status is { _sensor.DeviceStatusOk }", _sensor.DeviceStatusOk);
            }

            return output;
        }

        private void SendWeatherData(WeatherReading reading)
        {
            _server.CreateWeatherReadingAsync(reading);
        }

        public void IncrementFailedSensorCount(int i)
        {
            FailedSensorReadCount += i;
        }

        private void GetSettings()
        {
            string units = _config.GetValue<string>("Units");
            string deviceId = _config.GetValue<string>("DeviceID");
            int weatherSensorReadInterval = _config.GetValue<int>("WeatherSensorReadInterval");
            int ledBlinkInterval = _config.GetValue<int>("LedBlinkInterval");
            _units = units;
            _deviceId = deviceId;
            _weatherSensorReadInterval = weatherSensorReadInterval;
            _ledBlinkInterval = ledBlinkInterval;
        }
    }
}