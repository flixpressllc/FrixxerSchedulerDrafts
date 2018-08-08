namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class ScrollRectArea : IRectArea
    {
        public int Id { get; set; }

        /// <summary>
        /// "text" or "api"
        /// </summary>
        public string ScrollType { get; set; }
        
        /// <summary>
        /// If ScrollType = "text", this will be specified by the user. If it's "api", then
        /// it will be whatever value that the api per apiType will return.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// If ScrollType = "api", this will be filled with a string value that prompts
        /// what API should be called to fill this rect area. These are typically proper names
        /// of companies (or their specific pod casts) that issue RSS feeds.
        /// </summary>
        public string ApiType { get; set; }

        /// <summary>
        /// 2018/08/08
        /// If ScrollType = "api", this will be filled with a predefined RSS Url based on ApiType.
        /// If ScrollType = "customApi", we will let the user fill this field with a specific custom Url.
        /// </summary>
        public string ApiUrl { get; set; }

        public ScrollRectArea()
        {

        }
    }
}
