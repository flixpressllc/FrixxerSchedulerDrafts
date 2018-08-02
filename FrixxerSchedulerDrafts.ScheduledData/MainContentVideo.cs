using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class MainContentVideo
    {
        public string Name { get; set; }
        public long Duration { get; set; }
        public List<string> Tags { get; set; }
        public string Url { get; set; }

        public MainContentVideo()
        {
            Tags = new List<string>();
        }
    }
}
