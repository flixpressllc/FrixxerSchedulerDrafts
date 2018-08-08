using Frixxer.PresenterConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Frixxer.PresenterConsoleApp.Utils
{
    public static class RssTools
    {
        private static DateTime ParseDate(string date)
        {
            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return DateTime.MinValue;
        }

        private static string ExtractValue(XElement rootItem, string fieldName, string defaultValue)
        {
            XElement item = rootItem.Elements().FirstOrDefault(i => i.Name.LocalName == fieldName);

            if (item != null)
                return item.Value;

            return defaultValue;
        }

        public static List<RssFeedItem> ParseToGetRssFeedItems(string url)
        {
            List<RssFeedItem> articles = new List<RssFeedItem>();

            XDocument doc = XDocument.Load(url);
            var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                          select new RssFeedItem
                          {
                              Content = ExtractValue(item, "description", "default description"),
                              Link = ExtractValue(item, "link", "http://..."),                           
                              PublishDate = ParseDate(ExtractValue(item, "pubDate", "no pub date")),
                              Title = ExtractValue(item, "title", "default title")
                          };
            articles = entries.ToList();

            return articles;
        }
    }
}
