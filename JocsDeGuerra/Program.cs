using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JocsDeGuerra
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            builder.Services.AddScoped<IMapLocationService, MapLocationService>();
            builder.Services.AddScoped<IAssetService, AssetService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ITurnService, TurnService>();
            builder.Services.AddScoped<IApiService, ApiService>();
            builder.Services.AddScoped<IResetSystemService, AdminSystemService>();

            builder.Services.AddHttpClient(NamedClients.FIREBASE_CLIENT,
                client => { 
                    client.BaseAddress = new Uri("https://jocsdeguerra-e3130-default-rtdb.europe-west1.firebasedatabase.app/");
                });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
}
