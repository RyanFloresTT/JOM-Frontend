using blzr_frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<CartService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp =>
    new HttpClient {
        BaseAddress = new Uri("http://localhost:4242/")
    });

await builder.Build().RunAsync();
