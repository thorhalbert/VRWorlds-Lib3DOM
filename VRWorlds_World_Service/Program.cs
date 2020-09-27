using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configBuilder) =>
            {
                configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configBuilder.AddJsonFile("appsettings.json", optional: true);
                configBuilder.AddJsonFile(
                    $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                    optional: true);
                configBuilder.AddEnvironmentVariables();
            })
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                configLogging.AddConsole();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Here goes your internal application dependencies
                // like EntityFramework context, worker, endpoint, etc.
            });

        await hostBuilder.RunConsoleAsync();
    }
}