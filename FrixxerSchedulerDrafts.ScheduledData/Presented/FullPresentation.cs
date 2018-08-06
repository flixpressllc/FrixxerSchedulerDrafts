using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData.Presented
{
    public class FullPresentation
    {
        public long Duration { get; set; }
        public List<VideoPresentation> Videos { get; set; }

        public FullPresentation()
        {
            Videos = new List<VideoPresentation>();
        }
    }
}
