using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class DnsSeeds
{
    public const int NumberOfPopClientMessages = 200;
    public static readonly DnsDTOPageQueryResultDTO DnsListSeed = new();
    
    static DnsSeeds()
    {
        Random randomNum = new Random();
        
        var dnsFlagsFaker = new Faker<DnsFlags>()
            .RuleFor(o => o.OpCode, f => f.Random.Enum<DnsFlags.OpCodeEnum>())
            .RuleFor(o => o.Authoritative, f => f.Random.Bool())
            .RuleFor(o => o.Truncated, f => f.Random.Bool())
            .RuleFor(o => o.RecursionDesired, f => f.Random.Bool())
            .RuleFor(o => o.RecursionAvailable, f => f.Random.Bool())
            .RuleFor(o => o.AnswerAuthenticated, f => f.Random.Bool())
            .RuleFor(o => o.NonAuthenticatedData, f => f.Random.Bool());

        var dnsQuestionsFaker = new Faker<DnsQuestion>()
            .RuleFor(o => o.Name, f => f.Internet.Url())
            .RuleFor(o => o.Type, f => f.Random.Enum<DnsQuestion.TypeEnum>());
        
        var dnsResourceFaker = new Faker<DnsResourceRecord>()
            .RuleFor(o => o.Name, f => f.Internet.Url())
            .RuleFor(o => o.Target, f => f.Internet.Url())
            .RuleFor(o => o.Type, f => f.Random.Enum<DnsResourceRecord.TypeEnum>());

        var dnsFaker = new Faker<DnsDTO>()
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.Type, f => f.Random.Enum<DnsDTO.TypeEnum>())
            .RuleFor(o => o.Flags, f => dnsFlagsFaker.Generate(1)[0])
            .RuleFor(o => o.ReplyCode, f => f.Random.Enum<DnsDTO.ReplyCodeEnum>())
            .RuleFor(o => o.TransactionId, f => randomNum.Next(1000000, 8000000))
            .RuleFor(o => o.Timestamp, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(o => o.Questions, f => dnsQuestionsFaker.Generate(randomNum.Next(0, 10)))
            .RuleFor(o => o.Answers, f => dnsResourceFaker.Generate(randomNum.Next(0, 10)))
            .RuleFor(o => o.Authorities, f => dnsResourceFaker.Generate(randomNum.Next(0, 10)))
            .RuleFor(o => o.Additional, f => dnsResourceFaker.Generate(randomNum.Next(0, 10)));

        DnsListSeed.Items = dnsFaker.Generate(NumberOfPopClientMessages);
    }
}