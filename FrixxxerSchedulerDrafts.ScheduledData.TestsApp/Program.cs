﻿using FrixxerSchedulerDrafts.ScheduledData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace FrixxxerSchedulerDrafts.ScheduledData.TestsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore

            };

            ScheduledBlockData data = ScheduledBlockDataCreator.CreateA();

            string serialized = JsonConvert.SerializeObject(data, jsonSerializerSettings);

            Console.WriteLine(serialized);

            FileProcessor fp = new FileProcessor();

            fp.Write("C:/FrixxerFiles/sched1.json", serialized);

            ScheduledBlockData dataB = ScheduledBlockDataCreator.CreateB();

            string serializedB = JsonConvert.SerializeObject(dataB, jsonSerializerSettings);

            Console.WriteLine(serializedB);

            fp.Write("C:/FrixxerFiles/sched2.json", serializedB);

            /*
            We already know that this works, so we'll comment it out.

            ScheduledBlockData redeserialized = JsonConvert.DeserializeObject<ScheduledBlockData>(serialized, jsonSerializerSettings);

            string serialized2 = JsonConvert.SerializeObject(redeserialized, jsonSerializerSettings);

            Console.WriteLine(serialized2);
            */

            Console.ReadLine();
        }
    }
}
