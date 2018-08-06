namespace Frixxer.PresenterConsoleApp.Services
{
    public interface IDownloadService
    {
        /// <summary>
        /// Supposed to return the local path!
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string Download(string url);
    }
}
