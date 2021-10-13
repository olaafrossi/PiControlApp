// Created: 2021|10|13
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|IWeatherSensor.cs|PiControlApp
// Olaaf Rossi

using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI
{
    public interface IWeatherSensor
    {
        WeatherReading GetAllReadingsImperial();
    }
}