using Frixxer.PresenterConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Timers;

namespace Frixxer.PresenterConsoleApp
{
    public static class Execution
    {
        private static Timer Timer { get; set; }
        private static IConfigurationRoot Configuration { get; set; }
        private static ServiceProvider ServiceProvider { get; set; }

        public static void Start(IConfigurationRoot configuration, ServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;

            Timer = new Timer(Convert.ToInt32(Configuration["PollingFrequency"]) * 1000);
            Timer.Elapsed += ExecuteOneIteration;
            Timer.Start();
        }

        private static void ExecuteOneIteration(object sender, ElapsedEventArgs e)
        {
            ITimeLogProvider timeLogProvider = ServiceProvider.GetService<ITimeLogProvider>();
            Console.WriteLine($"Executing: { timeLogProvider.GenerateCurrentTimeLog() }");
        }
    }
}
