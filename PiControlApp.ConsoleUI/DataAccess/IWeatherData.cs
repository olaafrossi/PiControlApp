// Created: 2021|10|13
// Modified: 2021|10|13
// PiControlApp.ConsoleUI|IWeatherData.cs|PiControlApp
// Olaaf Rossi

using System.Threading.Tasks;
using PiControlApp.ConsoleUI.Models;
using Refit;

namespace PiControlApp.ConsoleUI.DataAccess
{
    public interface IWeatherData
    {
        [Post("/Weather")]
        Task CreateWeatherReadingAsync([Body] WeatherReading weather);
    }
}