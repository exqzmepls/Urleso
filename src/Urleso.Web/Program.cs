using Urleso.Web.Components;
using MudBlazor.Services;
using Serilog;
using Urleso.Web.Api;

var builder = WebApplication.CreateBuilder(args);

// https://github.com/MudBlazor/MudBlazor/issues/6724
builder.WebHost.UseStaticWebAssets();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
services.AddRazorComponents().AddInteractiveServerComponents();
services.AddMudServices();

services.AddApiSettings();
services.AddApiServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseSerilogRequestLogging();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();