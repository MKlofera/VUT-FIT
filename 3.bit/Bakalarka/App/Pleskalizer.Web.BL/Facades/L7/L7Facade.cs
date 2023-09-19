using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L7;

public class L7Facade : IAppFacade
{
    public readonly IL7ApiClient ApiClient;

    public L7Facade(IL7ApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task<L7ConversationStatisticsListDTOPageQueryResultDTO> GetAll(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.L7Async(caseId, pagingState, pageSize);
    }
    
    public async Task<L7ConversationStatisticsDetailDTO> GetL7Detail(Guid caseId, Guid captureId, string addressA, string addressB, string protocolL4, int portA, int portB, Guid sessionId)
    {
        return await ApiClient.L72Async(caseId, captureId, addressA, addressB, protocolL4, portA, portB, sessionId);
    }
    
    public async Task<L7ConversationStatisticsDetailDTO> GetL7DetailAggregated(Guid caseId, Guid captureId, 
        string addressA, string addressB, string protocolL4, int portA, int portB, Guid sessionId,
        DateTimeOffset? aggregateFrom, DateTimeOffset? aggregateTo)
    {
        return await ApiClient.Aggregate4Async(caseId, captureId, addressA, addressB, protocolL4, portA, portB, sessionId, aggregateFrom, aggregateTo);
    }
}