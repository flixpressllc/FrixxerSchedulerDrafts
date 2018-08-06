using System;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class TimeLogProvider : ITimeLogProvider
    {
        public string GenerateCurrentTimeLog()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
        }
    }
}
