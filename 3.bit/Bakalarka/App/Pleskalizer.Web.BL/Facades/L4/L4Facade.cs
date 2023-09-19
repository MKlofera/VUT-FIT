using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L4;

public class L4Facade : IAppFacade
{
    public readonly IL4ApiClient ApiClient;

    public L4Facade(IL4ApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task<L4ConversationStatisticsListDTOPageQueryResultDTO> GetAll(Guid caseId, byte[] paggingState, int pageSize)
    {
        return await ApiClient.L4Async(caseId, paggingState, pageSize);
    }
    
    public async Task<L4ConversationStatisticsDetailDTO> GetL4Detail(Guid caseId, Guid captureId, string addressA, string addressB, string protocolL4, int portA, int portB)
    {
        return await ApiClient.L42Async(caseId, captureId, addressA, addressB, protocolL4, portA, portB);
    }
    
    public async Task<L4ConversationStatisticsDetailDTO> GetL4DetailAggregated(Guid caseId, Guid captureId, 
        string addressA, string addressB, string protocolL4, int portA, int portB, 
        DateTimeOffset? aggregateFrom, DateTimeOffset? aggregateTo)
    {
        return await ApiClient.Aggregate3Async(caseId, captureId, addressA, addressB, protocolL4, portA, portB, aggregateFrom, aggregateTo);
    }
}