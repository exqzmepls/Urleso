using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using Urleso.Web.Api;
using Urleso.Web.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddMudServices();

services.AddApiSettings();
services.AddApiServices();

var host = builder.Build();

// WebAssemblyHost does not run startup validators, so resolve the settings eagerly to fail fast
_ = host.Services.GetRequiredService<IOptions<ApiSettings>>().Value;

await host.RunAsync();
