using FCore.Foundations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class DownloadService : IDownloadService
    {
        private IConfiguration Configuration { get; set; }

        public DownloadService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Download(string url)
        {
            string extension = Path.GetExtension(url);
            string saveToPath = $"{Configuration["FrixxerDownloadToRoot"]}{ Randomizer.GenerateString(12) }{extension}";

            /* We'll perform the actual download here. We'll do it later.
             */

            return saveToPath;
        }
    }
}
