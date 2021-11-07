// Created: 2021|11|07
// Modified: 2021|11|07
// PiControlApp.ConsoleUI|IMonitorService.cs|PiControlApp
// Olaaf Rossi

using PiControlApp.ConsoleUI.Models;

namespace PiControlApp.ConsoleUI
{
    public interface IMonitorService
    {
        bool Run();
        int FailedSensorReadCount { get; }
        bool RunLoop { get; set; }
        WeatherReading CurrentWeatherReading { get; set; }
    }
}