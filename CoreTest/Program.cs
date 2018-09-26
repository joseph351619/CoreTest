using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Formatting.Compact;

namespace CoreTest
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //using (var log = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    //.Enrich.FromLogContext()
            //    .WriteTo.File(new CompactJsonFormatter(), "log.clef")
            //    .CreateLogger())
            //{
            //    log.Information("Hello", "Serilog");
            //    log.Warning("Goodbye, Serilog");
            //    log.Debug("Processing item");
            //    var itemCount = 99;
            //    for (var itemNumber = 0; itemNumber < itemCount; ++itemNumber)
            //        log.Debug($"Processing item {itemNumber} of {itemCount}");

            //}

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(new CompactJsonFormatter(), "log.clef")
            .CreateLogger();

            var itemCount = 99;
            for (var itemNumber = 0; itemNumber < itemCount; ++itemNumber)
                Log.Debug("Processing item {ItemNumber} of {ItemCount}", itemNumber, itemCount);

            Log.CloseAndFlush();

            //CreateWebHostBuilder(args).Build().Run();

            //var configuration = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .CreateLogger();
            try
            {
                Log.Information("Application start....");
                //var host = new WebHostBuilder()
                //    .UseKestrel()
                //    .UseContentRoot(Directory.GetCurrentDirectory())
                //    .UseIISIntegration()
                //    .UseStartup<Startup>()
                //    .UseConfiguration(configuration)
                //    .UseUrls(configuration.GetSection("Setting").GetSection("Url").Value)
                //    .Build();
                //#region db seed
                //#endregion
                //host.Run();
                Log.CloseAndFlush();
            Console.Read();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
