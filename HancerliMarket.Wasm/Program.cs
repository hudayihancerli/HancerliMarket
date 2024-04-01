using Blazored.LocalStorage;
using Blazored.Toast;
using HancerliMarket.Services.Client;
using HancerliMarket.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddSingleton<Global>();
builder.Services.AddScoped<UserClient>();
builder.Services.AddScoped<BasketClient>();
builder.Services.AddScoped<ProductClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
