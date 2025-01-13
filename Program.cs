using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using personal_website.Components;
using personal_website.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = builder.HostEnvironment.BaseAddress;
var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
builder.Services.AddScoped(sp => httpClient);

// Add Security Service
builder.Services.AddScoped<SecurityService>();

// Configure lazy loading
builder.Services.AddScoped<LazyAssemblyLoader>();

try
{
    var host = builder.Build();
    
    // Resolve SecurityService and configure security
    var securityService = host.Services.GetRequiredService<SecurityService>();
    securityService.ConfigureHttpClientSecurity();

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Application startup error: {ex}");
    throw;
}
