using AutoMapper;
using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class CaptureSeeds
{
    public const int NumberOfCaptures = CaseSeeds.NumberOfCases * 10;

    public static List<CaptureConversationStatisticsListDTO> CaptureListSeed =
        new List<CaptureConversationStatisticsListDTO>();

    public static readonly List<CaptureConversationStatisticsDetailDTO> CaptureDetailSeedsList =
        new List<CaptureConversationStatisticsDetailDTO>();

    public static readonly CaptureConversationStatisticsListDTO CaptureListSeed1 = new(
        captureId: Guid.Parse("118cbd45-25d9-4dbd-a888-02eb759b7a9f"),
        uri: "www.seznam.com"
    );

    public static readonly CaptureConversationStatisticsListDTO CaptureListSeed2 = new(
        captureId: Guid.Parse("228cbd45-25d9-4dbd-a888-02eb759b7a9f"),
        uri: "www.google.com"
    );

    public static readonly CaptureConversationStatisticsDetailDTO CaptureDetailSeed1 = new(
        captureId: Guid.Parse("118cbd45-25d9-4dbd-a888-02eb759b7a9f"),
        uri: "www.seznam.com"
    );

    public static readonly CaptureConversationStatisticsDetailDTO CaptureDetailSeed2 = new(
        captureId: Guid.Parse("228cbd45-25d9-4dbd-a888-02eb759b7a9f"),
        uri: "www.google.com"
    );


    static CaptureSeeds()
    {
        Random randomNub = new Random();
        
        var caputerFaker = new Faker<CaptureConversationStatisticsDetailDTO>()
            .RuleFor(o => o.CaptureId, f => Guid.NewGuid())
            .RuleFor(o => o.Uri, f => f.Internet.Url())
            .RuleFor(o=>o.FlowStatisticsSnapshots, f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNub.Next(10,100))!);
            CaptureDetailSeedsList = caputerFaker.Generate(NumberOfCaptures);
            foreach (var capture in CaptureDetailSeedsList)
            {
                CaptureConversationStatisticsListDTO listDto = new();
                listDto.Uri = capture.Uri;
                listDto.CaptureId = capture.CaptureId;
                CaptureListSeed.Add(listDto);
            }

        CaptureDetailSeedsList.Add(CaptureDetailSeed1);
        CaptureDetailSeedsList.Add(CaptureDetailSeed2);
    }
}