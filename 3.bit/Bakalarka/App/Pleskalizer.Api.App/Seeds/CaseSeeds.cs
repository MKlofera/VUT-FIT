using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class CaseSeeds
{
    public const int NumberOfCases = 5;
    public static readonly CaseListDTOPageQueryResultDTO CaseList = new CaseListDTOPageQueryResultDTO();
    
    public static readonly CaseDetailDTO CaseDetail1 = new(
        id: Guid.Parse("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"),
        name: "Network Puzzle",
        description: "this is case to create use case"
    );
    public static readonly CaseDetailDTO CaseDetail2 = new(
        id: Guid.Parse("22cf3324-14b1-4c5e-8547-b5d0319ad2f1"),
        name: "Empty Case",
        description: "this is empty case"
    );
    static CaseSeeds()
    {
        //generating data
        var caseFaker = new Faker<CaseDetailDTO>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Name, f => f.System.FileName())
            .RuleFor(o => o.Description, f => f.Name.JobDescriptor());
        CaseList.Items = caseFaker.Generate(NumberOfCases);
        AddCapturesToCases(CaseList.Items);
        
        //Manualy created data
        CaseDetail1.Captures.Add(CaptureSeeds.CaptureListSeed1);
        CaseDetail1.Captures.Add(CaptureSeeds.CaptureListSeed2);

        CaseList.Items.Add(CaseDetail1);
        CaseList.Items.Add(CaseDetail2);
    }

    private static void AddCapturesToCases(List<CaseDetailDTO> data)
    {
        var capturePerCase = CaptureSeeds.NumberOfCaptures / NumberOfCases;
        
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = i * capturePerCase; j < i * capturePerCase + capturePerCase; j++)
            {
                if (j >= CaptureSeeds.CaptureListSeed.Count)
                {
                    continue;
                }
                data[i].Captures.Add(CaptureSeeds.CaptureListSeed[j]);
            }
        }
    }
}