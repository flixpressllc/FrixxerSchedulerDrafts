using Frixxer.EntityFramework;
using Frixxer.PresenterConsoleApp.Models;
using Frixxer.PresenterConsoleApp.Services;
using Frixxer.PresenterConsoleApp.Services.Scrolls;
using Frixxer.PresenterConsoleApp.Utils;
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

            ExecuteOneIteration(new { something = 3 }, null);
            
            /*
            Timer = new Timer(Convert.ToInt32(Configuration["PollingFrequency"]) * 1000);
            Timer.Elapsed += ExecuteOneIteration;
            Timer.Start();    
            */
        }

        private static void ExecuteOneIteration(object sender, ElapsedEventArgs e)
        {
            ITimeLogProvider timeLogProvider = ServiceProvider.GetService<ITimeLogProvider>();
            IFrixxerService frixxerService = ServiceProvider.GetService<IFrixxerService>();
            IAdsService adsService = ServiceProvider.GetService<IAdsService>();
            IDownloadService downloadService = ServiceProvider.GetService<IDownloadService>();
            IScrollApiProviderFactory scrollApiProviderFactory = ServiceProvider.GetService<IScrollApiProviderFactory>();
            FileProcessor fileProcessor = ServiceProvider.GetService<FileProcessor>();
            List<PresentationViewModel<Presentation>> presentations = frixxerService.GetPresentations(DateTime.Now, 300);

            List<FullPresentation> allPresentations = new List<FullPresentation>();

            presentations.ForEach(presentation =>
            {
                // Manage MainContent
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

                ManageScrollTextsForPresentation(fullPresentation, presentation.ScheduledBlockData, scrollApiProviderFactory);
                ManageStaticContent(fullPresentation, presentation.ScheduledBlockData, downloadService);
                ManageWidgets(fullPresentation, presentation.ScheduledBlockData);

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

        private static void ManageScrollTextsForPresentation(
            FullPresentation fullPresentation,
            ScheduledBlockData scheduledBlockData,
            IScrollApiProviderFactory scrollApiProviderFactory)
        {
            List<ScrollRectArea> scrollRectAreas = scheduledBlockData.RectAreas.Where(ra => (ra as ScrollRectArea) != null).Select(ra => ra as ScrollRectArea).ToList();

            scrollRectAreas.ForEach(scrollRectArea =>
            {
                if (scrollRectArea.ScrollType == ScrollRectAreaTypes.Text)
                    fullPresentation.ScrollTexts.Add(scrollRectArea.Text);
                else
                {
                    /*
                    IScrollApiProvider scrollApiProvider = scrollApiProviderFactory.CreateScrrollApiProviderInstance(scrollRectArea.ApiType);

                    if (scrollApiProvider != null)
                        fullPresentation.ScrollTexts.Add(scrollApiProvider.GetScrollText());
                    else
                        fullPresentation.ScrollTexts.Add($"Error: Scroll API Provider { scrollRectArea.ApiType } not found...");
                    */
                    try
                    {
                        List<RssFeedItem> items = RssTools.ParseToGetRssFeedItems(scrollRectArea.ApiUrl);

                        fullPresentation.ScrollTexts.Add(String.Join("|", items.Take(7).Select(i => i.Title.Trim()).ToList()));
                    }
                    catch(Exception)
                    {
                        fullPresentation.ScrollTexts.Add($"Error reading from the provider { scrollRectArea.ApiType }.");
                    }
                }
            });
        }

        private static void ManageWidgets(
            FullPresentation fullPresentation,
            ScheduledBlockData scheduledBlockData
        )
        {
            List<WidgetRectArea> widgetRectAreas = scheduledBlockData.RectAreas.Where(ra => (ra as WidgetRectArea) != null).Select(ra => ra as WidgetRectArea).ToList();

            widgetRectAreas.ForEach(widgetRectArea => {
                fullPresentation.WidgetsLists.Add(widgetRectArea.WidgetItems);
            });
        }

        private static void ManageStaticContent(
            FullPresentation fullPresentation,
            ScheduledBlockData scheduledBlockData,
            IDownloadService downloadService)
        {
            StaticRectArea staticRectArea = scheduledBlockData.RectAreas.Where(ra => (ra as StaticRectArea) != null).Select(ra => ra as StaticRectArea).FirstOrDefault();

            if (staticRectArea != null)
                fullPresentation.StaticImageLocalPath = downloadService.Download(staticRectArea.ImageUrl);
        }
    }
}
