// Created: 2021|10|23
// Modified: 2021|10|23
// PiControlApp.ConsoleUI|IMonitorService.cs|PiControlApp
// Olaaf Rossi

namespace PiControlApp.ConsoleUI
{
    public interface IMonitorService
    {
        void Run();
        int FailedSensorReadCount { get; }
        bool RunLoop { get; set; }
    }
}