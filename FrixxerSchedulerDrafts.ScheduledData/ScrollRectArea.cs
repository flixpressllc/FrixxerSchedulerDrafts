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
        /// what API should be called to fill this rect area.
        /// </summary>
        public string ApiType { get; set; }

        public ScrollRectArea()
        {

        }
    }
}
