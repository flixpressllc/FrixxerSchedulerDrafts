using System;

namespace Frixxer.PresenterConsoleApp.Models
{
    public class RssFeedItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public RssFeedItem()
        {
        }
    }
}
