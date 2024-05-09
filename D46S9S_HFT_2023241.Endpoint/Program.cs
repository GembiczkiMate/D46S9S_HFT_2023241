using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace D46S9S_HFT_2023241.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();
            string fileName = "data.db";
            string filePath = Path.Combine(directory, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
