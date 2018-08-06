using Frixxer.PresenterConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Frixxer.PresenterConsoleApp
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }

        private static void Configure()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddSingleton<ITimeLogProvider, TimeLogProvider>();

            ServiceProvider = services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Configure();
            Execution.Start(Configuration, ServiceProvider);
            Console.ReadLine();
        }
    }
}
