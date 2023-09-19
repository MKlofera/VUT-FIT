using AutoMapper;
using Pleskalizer.Common.Enums;
using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class L4Seeds
{
    public const int NumberOfL4List = 100;
    public static readonly L4ConversationStatisticsListDTOPageQueryResultDTO L4ListSeed = new();
    public static readonly List<L4ConversationStatisticsDetailDTO> L4DetailListSeed = new();

    static L4Seeds()
    {
        Random randomNum = new Random();

        var L4ListFaker = new Faker<L4ConversationStatisticsDetailDTO>()
            .RuleFor(o => o.CaptureId, f => f.Random.Guid())
            .RuleFor(o => o.AddressA, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.AddressB, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.PortA, f => SharedDTOsSeeds.PortNumbers[randomNum.Next(0, SharedDTOsSeeds.PortNumbers.Length - 1)])
            .RuleFor(o => o.PortB, f => SharedDTOsSeeds.PortNumbers[randomNum.Next(0, SharedDTOsSeeds.PortNumbers.Length - 1)])
            .RuleFor(o => o.ProtocolL3, f => f.Random.Enum<ProtocolL3Enum>())
            .RuleFor(o => o.ProtocolL4, f => f.Random.Enum<ProtocolL4Enum>())
            .RuleFor(o => o.AToBFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(0, 10)))
            .RuleFor(o => o.BToAFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(0, 10)));
        L4DetailListSeed = L4ListFaker.Generate(NumberOfL4List);

        foreach (var item in L4DetailListSeed)
        {
            L4ConversationStatisticsListDTO listModel = new L4ConversationStatisticsListDTO()
            {
                CaptureId = item.CaptureId,
                AddressA = item.AddressA,
                AddressB = item.AddressB,
                PortA = item.PortA,
                PortB = item.PortB,
                ProtocolL3 = item.ProtocolL3,
                ProtocolL4 = item.ProtocolL4,
            };
            L4ListSeed.Items.Add(listModel);
        }
    }
}