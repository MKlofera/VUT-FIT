using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L7;

public class DnsFacade : IAppFacade
{
    public readonly IDnsApiClient ApiClient;

    public DnsFacade(IDnsApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task<DnsDTOPageQueryResultDTO> GetAll(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.DnsAsync(caseId, pagingState, pageSize);
    }
    
    public async Task<DnsDTOPageQueryResultDTO> GetAllWithSession(Guid caseId, byte[] pagingState, int pageSize, Guid sessionId)
    {
        return await ApiClient.Dns2Async(caseId, sessionId, pagingState, pageSize);
    }
}