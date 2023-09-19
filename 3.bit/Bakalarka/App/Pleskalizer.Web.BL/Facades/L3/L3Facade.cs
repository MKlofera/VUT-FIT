using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L3;

public class L3Facade : IAppFacade
{
    public readonly IL3ApiClient ApiClient;

    public L3Facade(IL3ApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task<L3ConversationStatisticsListDTOPageQueryResultDTO> GetAll(Guid caseId, byte[] paggingState, int pageSize)
    {
        return await ApiClient.L3Async(caseId, paggingState, pageSize);
    }
    
    public async Task<L3ConversationStatisticsDetailDTO> GetL3Detail(Guid caseId, Guid captureId, string addressA, string addressB)
    {
        return await ApiClient.L32Async(caseId, captureId, addressA, addressB);
    }
    
    public async Task<L3ConversationStatisticsDetailDTO> GetL3DetailAggregated(Guid caseId, Guid captureId, string addressA, string addressB, DateTimeOffset? aggregateFrom, DateTimeOffset? aggregateTo)
    {
        return await ApiClient.Aggregate2Async(caseId, captureId, addressA, addressB, aggregateFrom, aggregateTo);
    }
}