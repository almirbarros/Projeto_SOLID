using System.Globalization;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vaga.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5194/") });

var culturaBrasileira = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = culturaBrasileira;
CultureInfo.DefaultThreadCurrentUICulture = culturaBrasileira;

await builder.Build().RunAsync();
