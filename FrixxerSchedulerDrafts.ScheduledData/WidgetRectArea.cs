using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class WidgetRectArea : IRectArea
    {
        public int Id { get; set; }
        public List<WidgetItem> WidgetItems { get; set; }

        public WidgetRectArea()
        {
            WidgetItems = new List<WidgetItem>();
        }
    }
}
