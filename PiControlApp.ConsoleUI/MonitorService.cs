// Created: 2021|10|12
// Modified: 2021|10|12
// PiControlApp.ConsoleUI|MonitorService.cs|PiControlApp
// Olaaf Rossi

using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI
{
    public class MonitorService : IMonitorService
    {
        private readonly IConfiguration _config;
        private readonly IGpioDevice _led;
        private readonly ILogger<MonitorService> _log;
        private readonly IWeatherSensor _sensor;

        public MonitorService(ILogger<MonitorService> log, IConfiguration config, IWeatherSensor sensor, IGpioDevice led)
        {
            _log = log;
            _config = config;
            _sensor = sensor;
            _led = led;
        }

        public void Run()
        {
            string units = _config.GetValue<string>("Units");
            string deviceId = _config.GetValue<string>("DeviceID");
            string serverUrl = _config.GetValue<string>("Server");
            int weatherSensorReadInterval = _config.GetValue<int>("WeatherSensorReadInterval");

            _log.LogInformation("starting the run loop");
            _log.LogInformation("the units set are {units} the deviceID is {deviceID} the server URL is {serverURL} the read interval is {weatherSensorReadInterval}", units, deviceId, serverUrl, weatherSensorReadInterval);

            SignalRConnection signalRConnection = new(serverUrl);
            int count = 1;

            while (true)
            {
                //signalRConnection.SendMessageAsync(deviceId, $"{DateTime.Now.Millisecond} initial startup");
                //signalRConnection.SendIntAsync("cm01-Counter", count);

                _led.LedState(state: true);
                Thread.Sleep(200);
                _led.LedState(state: false);
                Thread.Sleep(200);

                count++;
                WeatherReading reading = _sensor.GetAllReadingsImperial();

                _log.LogInformation("Reading from Sensor {count}", count);

                Console.WriteLine($"Pressure is {reading.Pressure} Humidity is {reading.Humidity} Altitude is {reading.Altitude} Temperature is {reading.Temperature} Units are {reading.Units}");
                //signalRConnection.SendMessageAsync("pi01-Data", allReadings);

                Thread.Sleep(weatherSensorReadInterval);
            }
        }
    }
}