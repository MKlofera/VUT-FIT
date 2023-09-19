using Pleskalizer;
using Pleskalizer.Web.App;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Globalization;
using Pleskalizer.Web.BL;
using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;
using AutoMapper.Internal;
using Pleskalizer.Web.BL.Extensions;
using MudBlazor;

string defaultCulture = "cs";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
string? apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl);
builder.Services.AddAutoMapper(configuration =>
{
    configuration.Internal().MethodMappingEnabled = false;
}, typeof(WebBLInstaller));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl ?? builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient();

var app = builder.Build();

var jsRuntime = app.Services.GetRequiredService<IJSRuntime>();
var cultureString = (await jsRuntime.InvokeAsync<string>("blazorCulture.get")) ?? defaultCulture;

var culture = new CultureInfo(cultureString);

await jsRuntime.InvokeVoidAsync("blazorCulture.set", cultureString);
var localStorage = app.Services.GetRequiredService<ILocalStorageService>();

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;



await app.RunAsync();

