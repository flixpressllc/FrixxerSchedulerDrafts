using Frixxer.EntityFramework;
using Frixxer.PresenterConsoleApp.Services;
using FrixxerSchedulerDrafts.ScheduledData;
using FrixxerSchedulerDrafts.ScheduledData.Presented;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Frixxer.PresenterConsoleApp
{
    public static class Execution
    {
        private static Timer Timer { get; set; }
        private static IConfigurationRoot Configuration { get; set; }
        private static ServiceProvider ServiceProvider { get; set; }

        public static void Start(IConfigurationRoot configuration, ServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;

            Timer = new Timer(Convert.ToInt32(Configuration["PollingFrequency"]) * 1000);
            Timer.Elapsed += ExecuteOneIteration;
            Timer.Start();
        }

        private static void ExecuteOneIteration(object sender, ElapsedEventArgs e)
        {
            ITimeLogProvider timeLogProvider = ServiceProvider.GetService<ITimeLogProvider>();
            IFrixxerService frixxerService = ServiceProvider.GetService<IFrixxerService>();
            IAdsService adsService = ServiceProvider.GetService<IAdsService>();
            IDownloadService downloadService = ServiceProvider.GetService<IDownloadService>();
            FileProcessor fileProcessor = ServiceProvider.GetService<FileProcessor>();
            List<PresentationViewModel<Presentation>> presentations = frixxerService.GetPresentations(DateTime.Now, 300);

            List<FullPresentation> allPresentations = new List<FullPresentation>();

            presentations.ForEach(presentation =>
            {
                MainContentRectArea mainContentRectArea = presentation.ScheduledBlockData.RectAreas.Where(ra => (ra as MainContentRectArea) != null).First() as MainContentRectArea;

                FullPresentation fullPresentation = new FullPresentation();
                fullPresentation.Duration = mainContentRectArea.Videos.Sum(v => v.Duration);                

                mainContentRectArea.Videos.ForEach(video =>
                {
                    VideoPresentation videoPresentation = new VideoPresentation();
                    videoPresentation.LocalPath = downloadService.Download(video.Url);
                    videoPresentation.Duration = video.Duration;
                    videoPresentation.Ads = adsService.GetAds(video.Tags);
                    fullPresentation.Videos.Add(videoPresentation);
                });

                allPresentations.Add(fullPresentation);
            });

            //Console.WriteLine($"Executing: { timeLogProvider.GenerateCurrentTimeLog() }");
            Console.WriteLine($"Executing..................");

            string outputPath = $"{Configuration["FrixxerPresentationOutputRoot"]}{timeLogProvider.GenerateCurrentTimeLog()}.json";

            string serializedPresentations = JsonConvert.SerializeObject(allPresentations, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });

            fileProcessor.Write(outputPath, serializedPresentations);
        }
    }
}
