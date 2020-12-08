using System.Globalization;

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;

namespace ArkhenManufacturing.WebApp
{
    public class Program
    {
        public static void Main(string[] args) {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
