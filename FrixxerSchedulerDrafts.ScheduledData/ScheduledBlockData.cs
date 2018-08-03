using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class ScheduledBlockData
    {
        [JsonConverter(typeof(RectAreaConverter))]
        public List<IRectArea> RectAreas { get; set; }

        public ScheduledBlockData()
        {
            RectAreas = new List<IRectArea>();
        }

        public void AddRectArea(IRectArea rectArea)
        {
            RectAreas.Add(rectArea);
        }
    }
}
