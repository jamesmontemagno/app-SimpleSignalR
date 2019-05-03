using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;

namespace SimpleSignalR.MobileAppService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebhostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebhostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .ConfigureKestrel((c, o) =>
                   {

                   })
                   .ConfigureLogging(logging =>
                   {
                       logging.ClearProviders();
                       logging.AddConsole();
                   });
    }
}
