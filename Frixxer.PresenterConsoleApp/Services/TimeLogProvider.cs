using System;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class TimeLogProvider : ITimeLogProvider
    {
        public string GenerateCurrentTimeLog()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        public string GenerateDateBasedPath()
        {
            return $"{DateTime.Now.ToString("yyyy")}/{DateTime.Now.ToString("MM")}/";
        }
    }
}
