using Pleskalizer.Web.BL.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Pleskalizer.Web.BL;

public class WebBLInstaller
{
    public void Install(IServiceCollection serviceCollection, string? apiBaseUrl)
    {
        serviceCollection.AddTransient<ICaseApiClient, CaseApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new CaseApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<ICaptureApiClient, CaptureApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new CaptureApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IContentApiClient, ContentApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new ContentApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IDnsApiClient, DnsApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new DnsApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IHttpApiClient, HttpApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new HttpApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IL3ApiClient, L3ApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new L3ApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IL4ApiClient, L4ApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new L4ApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IL7ApiClient, L7ApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new L7ApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<IPopApiClient, PopApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new PopApiClient(apiBaseUrl, client);
        });
        serviceCollection.AddTransient<ISmtpApiClient, SmtpApiClient>(provider =>
        {
            var client = CreateApiHttpClient(apiBaseUrl);
            return new SmtpApiClient(apiBaseUrl, client);
        });


        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<WebBLInstaller>()
                .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
    }


    public HttpClient CreateApiHttpClient(string? apiBaseUrl)
    {
        var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
        client.BaseAddress = new Uri(apiBaseUrl);
        return client;
    }
}