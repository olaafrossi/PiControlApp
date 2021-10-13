// Created: 2021|10|12
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|MonitorService.cs|PiControlApp
// Olaaf Rossi

using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PiControlApp.ConsoleUI.DataAccess;
using PiControlApp.ConsoleUI.Devices;
using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI
{
    public class MonitorService : IMonitorService
    {
        private readonly IConfiguration _config;
        private readonly IGpioDevice _led;
        private readonly ILogger<MonitorService> _log;
        private readonly IWeatherSensor _sensor;
        private readonly IWeatherData _server;

        public MonitorService(ILogger<MonitorService> log, IConfiguration config, IWeatherSensor sensor, IGpioDevice led, IWeatherData server)
        {
            _log = log;
            _config = config;
            _sensor = sensor;
            _led = led;
            _server = server;
        }

        public void Run()
        {
            string units = _config.GetValue<string>("Units");
            string deviceId = _config.GetValue<string>("DeviceID");
            int weatherSensorReadInterval = _config.GetValue<int>("WeatherSensorReadInterval");
            
            _log.LogInformation("starting the run loop");
            _log.LogInformation("the units set are {units} the deviceID is {deviceID} the read interval is {weatherSensorReadInterval}", units, deviceId, weatherSensorReadInterval);

            int count = 1;

            while (true)
            {
                LedBlink();
                count++;

                WeatherReading reading = _sensor.GetAllReadingsImperial();

                if (reading is not null)
                {
                    _log.LogInformation("Reading from Sensor {count}", count);
                    Console.WriteLine($"Pressure is {reading.Pressure} Humidity is {reading.Humidity} Altitude is {reading.Altitude} Temperature is {reading.Temperature} Units are {reading.Units}");

                    // post to server
                    _server.CreateWeatherReadingAsync(reading);
                }
                else
                {
                    _log.LogError("Could not read from Weather Sensor");
                    _log.LogError("The count is {count}", count);
                }

                Thread.Sleep(weatherSensorReadInterval);
            }
        }

        private void LedBlink()
        {
            int ledBlinkInterval = _config.GetValue<int>("LedBlinkInterval");
            _led.LedState(true);
            Thread.Sleep(ledBlinkInterval);
            _led.LedState(false);
            Thread.Sleep(ledBlinkInterval);
        }
    }
}