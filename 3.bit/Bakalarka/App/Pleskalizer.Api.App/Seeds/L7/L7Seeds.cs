using AutoMapper;
using Pleskalizer.Common.Enums;
using Bogus;
using IO.Swagger.Models;

namespace Pleskalizer.Api.DAL.Seeds;

public static class L7Seeds
{
    public const int NumberOfL7List = 1000;
    public static readonly L7ConversationStatisticsListDTOPageQueryResultDTO L7ListSeed = new();
    public static readonly List<L7ConversationStatisticsDetailDTO> L7DetailListSeed = new();

    static L7Seeds()
    {
        Random randomNum = new Random();

        var L7ListFaker = new Faker<L7ConversationStatisticsDetailDTO>()
            .RuleFor(o => o.CaptureId, f => f.Random.Guid())
            .RuleFor(o => o.AddressA, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.AddressB, f => SharedDTOsSeeds.IpAddresses[randomNum.Next(0, SharedDTOsSeeds.IpAddresses.Length - 1)])
            .RuleFor(o => o.PortA, f => SharedDTOsSeeds.PortNumbers[randomNum.Next(0, SharedDTOsSeeds.PortNumbers.Length - 1)])
            .RuleFor(o => o.PortB, f => SharedDTOsSeeds.PortNumbers[randomNum.Next(0, SharedDTOsSeeds.PortNumbers.Length - 1)])
            .RuleFor(o => o.ProtocolL3, f => f.Random.Enum<ProtocolL3Enum>())
            .RuleFor(o => o.ProtocolL4, f => f.Random.Enum<ProtocolL4Enum>())
            .RuleFor(o => o.ProtocolL7, f => f.PickRandom(SharedDTOsSeeds.L7Protocols))
            .RuleFor(o => o.SessionId, f => f.PickRandom(SharedDTOsSeeds.GuidArray))
            .RuleFor(o => o.AToBFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(10, 100)))
            .RuleFor(o => o.BToAFlowStatisticsSnapshots,
                f => SharedDTOsSeeds.FlowStatisticsSnapshotFaker.Generate(randomNum.Next(10, 100)));
        L7DetailListSeed = L7ListFaker.Generate(NumberOfL7List);

        foreach (var item in L7DetailListSeed)
        {
            L7ConversationStatisticsListDTO listModel = new L7ConversationStatisticsListDTO()
            {
                CaptureId = item.CaptureId,
                AddressA = item.AddressA,
                AddressB = item.AddressB,
                PortA = item.PortA,
                PortB = item.PortB,
                ProtocolL3 = item.ProtocolL3,
                ProtocolL4 = item.ProtocolL4,
                ProtocolL7 = item.ProtocolL7,
                SessionId = item.SessionId
            };
            L7ListSeed.Items.Add(listModel);
        }
    }
}