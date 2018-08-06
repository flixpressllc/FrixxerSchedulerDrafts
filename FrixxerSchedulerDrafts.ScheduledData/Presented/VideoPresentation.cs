using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData.Presented
{
    public class VideoPresentation
    {
        public long Duration { get; set; }
        public string LocalPath { get; set; }
        public List<AdPresentation> Ads { get; set; }

        public VideoPresentation()
        {
            Ads = new List<AdPresentation>();
        }
    }
}
