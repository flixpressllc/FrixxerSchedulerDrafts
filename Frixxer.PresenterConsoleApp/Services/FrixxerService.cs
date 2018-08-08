using Frixxer.EntityFramework;
using FrixxerSchedulerDrafts.ScheduledData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Frixxer.PresenterConsoleApp.Services
{
    public class FrixxerService : IFrixxerService
    {
        private IConfiguration Configuration { get; set; }
        private FileProcessor FileProcessor { get; set; }

        public FrixxerService(IConfiguration configuration, FileProcessor fileProcessor)
        {
            Configuration = configuration;
            FileProcessor = fileProcessor;
        }

        public List<PresentationViewModel<Presentation>> GetPresentations(DateTime startTime, int durationInSeconds)
        {
            /* We'll later do the actual API call here. But for now, we're reading json files.
            */
            int whichPresentation = startTime.Second % 2 + 1;
            string whichFile = $"{Configuration["FrixxerFilesRoot"]}/sched{whichPresentation}.json";

            string fileContents = FileProcessor.ReadToString(whichFile);
            ScheduledBlockData data = JsonConvert.DeserializeObject<ScheduledBlockData>(fileContents, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            return new List<PresentationViewModel<Presentation>>
            {
                new PresentationViewModel<Presentation> { Id = -1, Name = $"Presentation { whichPresentation.ToString() }", ScheduledBlockData = data }
            };
        }
    }
}
