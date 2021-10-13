// Created: 2021|09|29
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|WeatherReading.cs|PiControlApp
// Olaaf Rossi

namespace PiControlApp.ConsoleUI.Models
{
    public class WeatherReading
    {
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Altitude { get; set; }
        public double Temperature { get; set; }
        public string Units { get; set; }
    }
}