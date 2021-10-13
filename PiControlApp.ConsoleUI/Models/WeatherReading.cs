// Created: 2021|09|29
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|WeatherReading.cs|PiControlApp
// Olaaf Rossi

namespace PiControlApp.ConsoleUI.Models
{
    public class WeatherReading
    {
        public double Pressure { get; init; }
        public double Humidity { get; init; }
        public double Altitude { get; init; }
        public double Temperature { get; init; }
        public string Units { get; init; }
    }
}