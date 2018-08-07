using FCore.Foundations;
using FCore.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class DownloadService : IDownloadService
    {
        private IConfiguration Configuration { get; set; }

        public DownloadService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        private string GetAcceptHeaderValue(string url)
        {
            if (url.EndsWith(".mp4"))
                return "video/mp4";
            else if (url.EndsWith(".jpg") || url.EndsWith(".jpeg"))
                return "image/jpeg";
            else if (url.EndsWith(".png"))
                return "image/png";
            else if (url.EndsWith(".avi"))
                return "video/x-msvideo";

            return null;
        }

        public string Download(string url)
        {
            string extension = Path.GetExtension(url);
            string saveToPath = $"{Configuration["FrixxerDownloadToRoot"]}{ Randomizer.GenerateString(12) }{extension}";

            /* We'll perform the actual download here. We'll do it later. We do not handle
             * whether there was any sort of error, though.
             */
            if (!File.Exists(saveToPath))
            {
                HttpResponseMessage response = HttpCalls.GetAsync(url,
                    acceptHeader: GetAcceptHeaderValue(url)).Result;

                Stream stream = response.Content.ReadAsStreamAsync().Result;

                using (FileStream fileStream = new FileStream(saveToPath, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyToAsync(fileStream).Wait();
                }
            }

            return saveToPath;
        }
    }
}
