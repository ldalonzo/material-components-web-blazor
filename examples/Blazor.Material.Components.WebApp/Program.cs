using Microsoft.AspNetCore.Blazor.Hosting;
using System;

namespace Blazor.Material.Components.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var assemblyName = typeof(Program).Assembly.GetName();
            Console.WriteLine($"Starting {assemblyName.Name} {assemblyName.Version}");
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
