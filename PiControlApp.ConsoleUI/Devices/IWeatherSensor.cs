// Created: 2021|10|27
// Modified: 2021|10|27
// PiControlApp.ConsoleUI|IWeatherSensor.cs|PiControlApp
// Olaaf Rossi

using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI.Devices
{
    public interface IWeatherSensor
    {
        void ConnectToSensor();
        WeatherReading ReadWeather();
        bool SensorStatusOk { get; }
    }
}