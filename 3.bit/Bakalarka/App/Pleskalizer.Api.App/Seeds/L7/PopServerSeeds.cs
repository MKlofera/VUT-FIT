using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class PopServerSeeds
{
    public const int NumberOfPopClientMessages = 200;
    public static readonly PopServerMessageDTOPageQueryResultDTO PopServerMessageListSeed = new();

    static PopServerSeeds()
    {
        Random randomNum = new Random();
        var PopServerMessage = new Faker<PopServerMessageDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Reply, f => f.Random.Enum<PopServerMessageDTO.ReplyEnum>())
            .RuleFor(o => o.Description, f => f.Commerce.ProductDescription())
            .RuleFor(o => o.Payload, f => f.Lorem.Paragraphs(randomNum.Next(1, 5)))
            .RuleFor(o => o.Envelope, f => SharedDTOsSeeds.EmailEnvelopeFaker.Generate(1)[0])
            .RuleFor(o => o.Email, f => SharedDTOsSeeds.EmailMessageFaker.Generate(1)[0])
            .RuleFor(o => o.MailPath, f => f.PickRandom(SharedDTOsSeeds.EmailPath))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.Attachments, f => SharedDTOsSeeds.EmailAttachmentFaker.Generate(randomNum.Next(0, 15)));
        PopServerMessageListSeed.Items = PopServerMessage.Generate(NumberOfPopClientMessages);
    }
}