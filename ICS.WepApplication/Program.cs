using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LightInject.Microsoft.DependencyInjection;

namespace ICS.WebAppCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .UseServiceProviderFactory(new LightInjectServiceProviderFactory())
               .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
