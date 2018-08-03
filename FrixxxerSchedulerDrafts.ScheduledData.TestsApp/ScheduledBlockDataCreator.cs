using FrixxerSchedulerDrafts.ScheduledData;
using System.Collections.Generic;

namespace FrixxxerSchedulerDrafts.ScheduledData.TestsApp
{
    public static class ScheduledBlockDataCreator
    {
        public static ScheduledBlockData CreateA()
        {
            ScheduledBlockData data = new ScheduledBlockData();

            data.AddRectArea(new StaticRectArea { Id = 2, ImageUrl = "http://www.abc.com/e92w3i2hjk3js.jpg" });
            data.AddRectArea(new WidgetRectArea()
            {
                Id = 7,
                WidgetItems = new List<WidgetItem>
                {
                    new WidgetItem { Duration = 30, Type = "weather"},
                    new WidgetItem { Duration = 210, Type = "ads"},
                    new WidgetItem { Duration = 60, Type = "sports"}
                }
            });
            data.AddRectArea(new ScrollRectArea { Id = 3, ScrollType = "text", Text = "this is what I want to say. " });
            data.AddRectArea(new ScrollRectArea { Id = 3, ScrollType = "api", Text = "the market stock what drop i don't know." });
            data.AddRectArea(new ScrollRectArea { Id = 3, ScrollType = "api", Text = "what is happening in the world today?" });
            data.AddRectArea(new MainContentRectArea
            {
                Id = 5,
                Videos = new List<MainContentVideo>
                {
                    new MainContentVideo { Duration = 30, Name = "Panda Wars", Tags = new List<string> { "animals", "zoo" }, Url = "http://www.cnn.com/2391293199s929199.mp4" },
                    new MainContentVideo { Duration = 30, Name = "Spelling Bee", Tags = new List<string> { "academics", "school", "talent" }, Url = "http://www.agt.com/2391293199s29.mp4" },
                    new MainContentVideo { Duration = 120, Name = "Moderate Network III", Tags = new List<string> { "reality", "truth" }, Url = "http://www.npr.com/1111129199.mp4" },
                    new MainContentVideo { Duration = 120, Name = "Cool Beans", Tags = new List<string> { "food", "health", "nutrition" }, Url = "http://www.food.com/222211111.mp4" },
                }
            });

            return data;          
        }

        public static ScheduledBlockData CreateB()
        {
            ScheduledBlockData data = new ScheduledBlockData();

            data.AddRectArea(new StaticRectArea { Id = 99, ImageUrl = "http://www.abc.com/e92w3i2hjk3js.jpg" });
            data.AddRectArea(new WidgetRectArea()
            {
                Id = 7,
                WidgetItems = new List<WidgetItem>
                {
                    new WidgetItem { Duration = 300, Type = "weather"},
                }
            });
            data.AddRectArea(new WidgetRectArea()
            {
                Id = 7,
                WidgetItems = new List<WidgetItem>
                {
                    new WidgetItem { Duration = 300, Type = "ads"},
                }
            });
            data.AddRectArea(new MainContentRectArea
            {
                Id = 5,
                Videos = new List<MainContentVideo>
                {
                    new MainContentVideo { Duration = 150, Name = "MRT 7 Complete", Tags = new List<string> { "metro", "infrastructure" }, Url = "http://www.abs-cbn.com/2391293199s929199.mp4" },
                    new MainContentVideo { Duration = 150, Name = "Aliens always existed", Tags = new List<string> { "conspiracy", "space" }, Url = "http://www.agt.com/2391293199s29.mp4" },                    
                }
            });

            return data;
        }

    }
}
