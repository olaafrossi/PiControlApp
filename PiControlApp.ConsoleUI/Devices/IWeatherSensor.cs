// Created: 2021|11|07
// Modified: 2021|11|07
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
        void Dispose();
    }
}