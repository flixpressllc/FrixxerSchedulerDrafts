using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData.Presented
{
    public class FullPresentation
    {
        public long Duration { get; set; }
        public List<VideoPresentation> Videos { get; set; }
        public List<string> ScrollTexts { get; set; }
        public string StaticImageLocalPath { get; set; }
             
        public FullPresentation()
        {
            Videos = new List<VideoPresentation>();
            ScrollTexts = new List<string>();
        }
    }
}
