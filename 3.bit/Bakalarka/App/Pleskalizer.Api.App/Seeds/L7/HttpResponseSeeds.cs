using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class HttpResponseSeeds
{
    public const int NumberOfHttpResponseMessages = 200;
    public static readonly HttpResponseDTOPageQueryResultDTO HttpResponseListSeed = new();

    static HttpResponseSeeds()
    {
        Random randomNumb = new Random();

        var lastIndexOfHttpHeaderNamesArray = SharedDTOsSeeds.HttpHeaderNames.Length - 1;
        var httpResponseFaker = new Faker<HttpResponseDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.StatusCode, f => f.PickRandom(SharedDTOsSeeds.HttpStatusCodes))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.Headers, f=> SharedDTOsSeeds.GetHttpHeaderFaker().Generate(randomNumb.Next(0, lastIndexOfHttpHeaderNamesArray)));
        HttpResponseListSeed.Items = httpResponseFaker.Generate(NumberOfHttpResponseMessages);
    }
}