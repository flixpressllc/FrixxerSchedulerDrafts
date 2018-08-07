namespace Frixxer.PresenterConsoleApp.Services
{
    public interface ITimeLogProvider
    {
        string GenerateCurrentTimeLog();
        string GenerateDateBasedPath();
    }
}
