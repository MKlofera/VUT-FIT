using Xunit.Abstractions;

namespace FoodDelivery.Api.App.EndToEndTests;

public abstract class ApiTest : IAsyncDisposable
{
    protected HttpClient Client => client.Value;

    readonly FoodDeliveryApiApplicationFactory application;
    readonly Lazy<HttpClient> client;

    protected ApiTest(ITestOutputHelper output)
    {
        application = new FoodDeliveryApiApplicationFactory(output);
        client = new Lazy<HttpClient>(application.CreateClient);
    }

    public async ValueTask DisposeAsync()
    {
        await application.DisposeAsync();
    }
}
