using FCore.Foundations;
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

        private string GetRandomAdUrl()
        {
            List<string> randomUrls = new List<string>();

            randomUrls.Add("https://mediaassets.theindychannel.com/photo/2017/10/14/GettyImages-859103968_1507998046184_68822459_ver1.0_640_480.jpg");
            randomUrls.Add("https://www.cebupacificair.com/assets/PublishingImages/CMS/en-ph/about-cebcargo/about-cargo.png");
            randomUrls.Add("https://upload.wikimedia.org/wikipedia/commons/7/70/Bonifacio_Global_City_2.jpg");
            randomUrls.Add("https://i.ytimg.com/vi/9R1N_ww10wE/maxresdefault.jpg");
            randomUrls.Add("https://images.ctfassets.net/7h71s48744nc/3o0AjJhmkE4m8UsCa4K0uq/aa6b9828dea96b50687546fdbfe11b2a/Fortnight__Large.jpg");
            randomUrls.Add("http://revelwallpapers.net/media/wallpapers/nonomiya-shrine-torii-gate-tokyo-japan-in-winter-season.jpg");
            randomUrls.Add("https://www.norwegian.com/magazine/contentFiles/image/2015/april/romsdal-720x390.jpg");
            randomUrls.Add("http://images.nymag.com/travel/2010/winter/travel101101_adventuring_2_560.jpg");
            randomUrls.Add("https://i.pinimg.com/originals/9f/ce/70/9fce7075f78934730c3a87b8a8ceba73.jpg");
            randomUrls.Add("https://pedleys.com.au/wp-content/uploads/2015/06/Powerful-Electrical-Upgrades.jpg");

            return randomUrls[Randomizer.GenerateRandomInteger(0, randomUrls.Count - 1)];
        }

        public List<AdPresentation> GetAds(List<string> tags)
        {
            string adsEndpoint = Configuration["GetAdsEndpoint"];

            Console.WriteLine($"... about to get ads from {adsEndpoint}");

            /* In reality, we will obtain the ads from an actual API call, which will include the actual URL
             */

            return new List<AdPresentation>
            {
                new AdPresentation {
                    Duration = 15,
                    LocalPath = DownloadService.Download(GetRandomAdUrl())
                }
            };
        }
    }
}
