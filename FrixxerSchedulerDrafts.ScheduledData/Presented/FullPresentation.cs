﻿using System.Collections.Generic;

namespace FrixxerSchedulerDrafts.ScheduledData.Presented
{
    public class FullPresentation
    {
        public long Duration { get; set; }
        public List<VideoPresentation> Videos { get; set; }
        public List<string> ScrollTexts { get; set; }
        public List<List<WidgetItem>> WidgetsLists { get; set; }
        public string StaticImageLocalPath { get; set; }
             
        public FullPresentation()
        {
            WidgetsLists = new List<List<WidgetItem>>();
            Videos = new List<VideoPresentation>();
            ScrollTexts = new List<string>();
        }
    }
}
