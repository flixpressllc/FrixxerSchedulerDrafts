﻿using FCore.Foundations;
using Frixxer.PresenterConsoleApp.Models;
using Frixxer.PresenterConsoleApp.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Frixxer.PresenterConsoleApp.Services.Scrolls
{
    public class NprScrollApiProvider : IScrollApiProvider
    {
        public string GetScrollText()
        {
            List<RssFeedItem> items = RssTools.ParseToGetRssFeedItems("https://www.npr.org/rss/podcast.php?id=510053");

            List<string> titles = items.Take(7).Select(i => i.Title).ToList();

            return StringTools.ToStringList(titles.ToArray());
        }
    }
}
