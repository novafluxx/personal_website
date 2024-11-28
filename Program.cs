using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using personal_website.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure lazy loading
builder.Services.AddScoped<LazyAssemblyLoader>();

try
{
    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Application startup error: {ex}");
    throw;
}
