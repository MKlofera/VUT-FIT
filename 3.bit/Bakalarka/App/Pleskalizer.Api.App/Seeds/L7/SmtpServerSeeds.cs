using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class SmtpServerSeeds
{
    public const int NumberOfSmtpServerMessages = 200;
    public static readonly SmtpServerMessageDTOPageQueryResultDTO SmtpServerMessageListSeed = new();

    static SmtpServerSeeds()
    {
        Random randomNum = new Random();

        int[] smtpReplyCodes = new int[] { 
            211, 214, 220, 221, 250, 
            251, 354, 421, 450, 451, 
            452, 500, 501, 502, 503, 
            504, 550, 551, 552, 553, 
            554 
        };

        var smtpServerMessage = new Faker<SmtpServerMessageDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Parameters,
                f => f.Make(randomNum.Next(0, 10), () => f.Internet.Email()))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.ReplyCode, f => f.PickRandom(smtpReplyCodes));
        SmtpServerMessageListSeed.Items = smtpServerMessage.Generate(NumberOfSmtpServerMessages);
        
    }
}