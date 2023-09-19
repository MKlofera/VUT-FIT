using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class PopClientSeeds
{
    public const int NumberOfPopClientMessages = 200;
    public static readonly PopClientMessageDTOPageQueryResultDTO PopClientMessageListSeed = new();

    static PopClientSeeds()
    {
        Random randomNum = new Random();

        var popClientMessage = new Faker<PopClientMessageDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Arguments,
                f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.Command, f => f.Random.Enum<PopClientMessageDTO.CommandEnum>());
        PopClientMessageListSeed.Items = popClientMessage.Generate(NumberOfPopClientMessages);
    }
}