using AutoMapper;
using Pleskalizer.Common.Enums;
using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class L3Seeds
{
    public const int NumberOfL3List = 100;
    public static readonly L3ConversationStatisticsListDTOPageQueryResultDTO L3ListSeed = new();
    public static readonly List<L3ConversationStatisticsDetailDTO> L3DetailListSeed = new();

    static L3Seeds()
    {
        Random randomNum = new Random();

        var L3ListFaker = new Faker<L3ConversationStatisticsDetailDTO>()
            .RuleFor(o => o.CaptureId, f => f.Random.Guid())
            .RuleFor(o => o.AddressA, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.AddressB, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.ProtocolL3, f => f.Random.Enum<ProtocolL3Enum>())
            .RuleFor(o => o.AToBFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(0, 100)))
            .RuleFor(o => o.BToAFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(0, 100)));
        L3DetailListSeed = L3ListFaker.Generate(NumberOfL3List);

        foreach (var item in L3DetailListSeed)
        {
            L3ConversationStatisticsListDTO listModel = new L3ConversationStatisticsListDTO()
            {
                CaptureId = item.CaptureId,
                AddressA = item.AddressA,
                AddressB = item.AddressB,
                ProtocolL3 = item.ProtocolL3,
            };
            L3ListSeed.Items.Add(listModel);
        }
    }
}