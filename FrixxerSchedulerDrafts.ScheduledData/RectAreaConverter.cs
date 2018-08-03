using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FrixxerSchedulerDrafts.ScheduledData
{
    public class RectAreaConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IRectArea);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rectAreas = new List<IRectArea>();
            var jArray = JArray.Load(reader);

            foreach(var item in jArray)
            {
                var jObject = item as JObject;
                var rectArea = default(IRectArea);

                // Investigation goes here.
                if (jObject["videos"] != null) // main content
                {
                    rectArea = new MainContentRectArea();
                }
                else if (jObject["scrollType"] != null) // scroll
                {
                    rectArea = new ScrollRectArea();
                }
                else if (jObject["imageUrl"] != null) // static
                {
                    rectArea = new StaticRectArea();
                }
                else if (jObject["widgetItems"] != null) // widget
                {
                    rectArea = new WidgetRectArea();
                }
                serializer.Populate(jObject.CreateReader(), rectArea);
                rectAreas.Add(rectArea);
            }

            return rectAreas;            
        }

    }
}
