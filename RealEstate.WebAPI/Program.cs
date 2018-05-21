namespace RealEstate.WebAPI
{
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            Serilog.Log.CloseAndFlush();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(ConfigConfiguration)
                .ConfigureLogging(ConfigureLogger)
                .Build();

        private static void ConfigConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            var env = ctx.HostingEnvironment;

            config.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
        }

        private static void ConfigureLogger(WebHostBuilderContext ctx, ILoggingBuilder logging)
        {
            var env = ctx.HostingEnvironment;
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(ctx.Configuration)
                .CreateLogger();

            logging.AddSerilog();

            var applicationInsightsInstrumentationKey = ctx.Configuration["appInsightsKey"];
            ConfigureApplicationInsightsLogging(applicationInsightsInstrumentationKey);

            if (env.IsDevelopment())
            {
                logging.AddConsole();
                logging.AddDebug();
            }

            Serilog.Log.Information("Env is " + env.EnvironmentName + " AppInsights key is: " + applicationInsightsInstrumentationKey);
        }

        private static void ConfigureApplicationInsightsLogging(string applicationInsightsInstrumentationKey)
        {
            Serilog.Log.Logger = new Serilog.LoggerConfiguration()
                    .MinimumLevel.Verbose() // Minimum severity to log
                    .WriteTo
                    .ApplicationInsightsEvents(applicationInsightsInstrumentationKey)
                    .CreateLogger();
        }
    }
}
