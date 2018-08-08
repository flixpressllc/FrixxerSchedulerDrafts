using FCore.Foundations;
using Frixxer.PresenterConsoleApp.Models;
using Frixxer.PresenterConsoleApp.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Frixxer.PresenterConsoleApp.Services.Scrolls
{
    public class CnnScrollApiProvider : IScrollApiProvider
    {
        public string GetScrollText()
        {  
            List<RssFeedItem> items = RssTools.ParseToGetRssFeedItems("http://rss.cnn.com/rss/cnn_topstories.rss");

            List<string> titles = items.Take(7).Select(i => i.Title).ToList();

            return StringTools.ToStringList(titles.ToArray());
        }
    }
}
