using Blazored.Modal;
using Lmbtfy.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Lmbtfy.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("TinyUrl", client =>
                client.BaseAddress = new Uri("https://tinyurl.com"));

            builder.Services.AddBlazoredModal();

            builder.Services.AddSingleton<IImageService, ImageService>();

            await builder.Build().RunAsync();
        }
    }
}
