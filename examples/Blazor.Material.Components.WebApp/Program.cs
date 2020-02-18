using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;

namespace Blazor.Material.Components.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var assemblyName = typeof(Program).Assembly.GetName();
            Console.WriteLine($"Starting {assemblyName.Name} {assemblyName.Version}");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
