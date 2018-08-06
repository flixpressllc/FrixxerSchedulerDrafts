using FrixxerSchedulerDrafts.ScheduledData.Presented;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class AdsService : IAdsService
    {
        private IConfiguration Configuration { get; set; }
        private IDownloadService DownloadService { get; set; }

        public AdsService(IConfiguration configuration, IDownloadService downloadService)
        {
            Configuration = configuration;
            DownloadService = downloadService;
        }

        public List<AdPresentation> GetAds(List<string> tags)
        {
            string adsEndpoint = Configuration["GetAdsEndpoint"];

            Console.WriteLine($"... about to get ads from {adsEndpoint}");

            return new List<AdPresentation>
            {
                new AdPresentation {
                    Duration = 15,
                    LocalPath = DownloadService.Download("http://www.cnn.com/bogusadurl.jpg")
                }
            };
        }
    }
}
