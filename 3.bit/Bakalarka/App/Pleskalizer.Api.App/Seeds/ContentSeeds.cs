using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class ContentSeeds
{
    public const int NumberOfContent = 5;
    public static readonly List<ContentSummaryDTO> ContentSummaryListSeeds = new List<ContentSummaryDTO>();
    
    static ContentSeeds()
    {
        Random randomNum = new Random();

        string[] dictionaryKeys = new string[10] { "Alza.cz", "Czc.cz", "Moneta.cz", "Csob.cz", "Amicci.cz", "BurgerKing.cz", "KFC.cz", "MCdonald.cz", "Fio.cz", "Microsoft.cz",  };
        var contentDictionary = new Dictionary<string, int?>();

        for (int i = 0; i < randomNum.Next(2,9); i++)
        {
            contentDictionary.Add(dictionaryKeys[i], randomNum.Next(1, 10000));
        }

        var ContentSummaryFaker = new Faker<ContentSummaryDTO>()
            .RuleFor(x => x.From, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(x => x.To, f => (int)DateTime.UtcNow
                .Subtract(f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)))
                .TotalSeconds)
            .RuleFor(x => x.Reports, f => randomNum.Next(0, 10000))
            .RuleFor(x => x.Contents, f => contentDictionary);
        ContentSummaryListSeeds = ContentSummaryFaker.Generate(CaseSeeds.NumberOfCases);
    }
}