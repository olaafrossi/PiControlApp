// Created: 2021|10|13
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|IWeatherSensor.cs|PiControlApp
// Olaaf Rossi

using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI.Devices
{
    public interface IWeatherSensor
    {
        /// <summary>
        ///     Gets all of the sensor readings in Imperial Units
        /// </summary>
        /// <returns></returns>
        WeatherReading GetAllReadingsImperial();
    }
}