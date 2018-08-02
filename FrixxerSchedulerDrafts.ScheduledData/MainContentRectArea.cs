using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class MainContentRectArea : IRectArea
    {
        public int Id { get; set; }

        public List<MainContentVideo> Videos { get; set; }

        public MainContentRectArea()
        {
            Videos = new List<MainContentVideo>();
        }
    }
}
