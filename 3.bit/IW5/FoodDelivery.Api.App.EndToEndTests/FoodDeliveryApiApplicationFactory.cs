using Divergic.Logging.Xunit;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace FoodDelivery.Api.App.EndToEndTests;

class FoodDeliveryApiApplicationFactory : WebApplicationFactory<Program>
{
    readonly ITestOutputHelper testOutput;

    public FoodDeliveryApiApplicationFactory(ITestOutputHelper output) => this.testOutput = output;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            collection.AddLogging(logging => logging.AddXunit(testOutput,
                new LoggingConfig { LogLevel = LogLevel.Warning }));
            collection.AddMvc().AddApplicationPart(typeof(Program).Assembly);
        });
        return base.CreateHost(builder);
    }
}
