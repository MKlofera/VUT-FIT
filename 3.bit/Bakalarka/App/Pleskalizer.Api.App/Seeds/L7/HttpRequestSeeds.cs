using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class HttpRequestSeeds
{
    public const int NumberOfHttpRequestMessages = 200;
    public static readonly HttpRequestDTOPageQueryResultDTO HttpRequestListSeed = new();

    static HttpRequestSeeds()
    {
        Random randomNumb = new Random();

        var lastIndexOfHttpHeaderNamesArray = SharedDTOsSeeds.HttpHeaderNames.Length - 1;
        var httpResponseFaker = new Faker<HttpRequestDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Method, f => f.Random.Enum<HttpRequestDTO.MethodEnum>())
            .RuleFor(o => o.Uri, f => SharedDTOsSeeds.Urls[randomNumb.Next(0,9)])
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.Headers, f=> SharedDTOsSeeds.GetHttpHeaderFaker().Generate(randomNumb.Next(0, lastIndexOfHttpHeaderNamesArray)));
        HttpRequestListSeed.Items = httpResponseFaker.Generate(NumberOfHttpRequestMessages);
    }
}