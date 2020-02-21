using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Proyecto1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            CreateWebHostBuilder(args).Run();

        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
              .UseUrls("http://*:8090")
               .UseKestrel((context, options) =>
               {
                   // Set properties and call methods on options
               })
               .UseIISIntegration()
               .UseEnvironment("Development")
               .UseStartup<Startup>()
               .Build()
            ;

    }
}
