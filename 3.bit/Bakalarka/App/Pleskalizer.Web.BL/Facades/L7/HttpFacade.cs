using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L7;

public class HttpFacade : IAppFacade
{
    public readonly IHttpApiClient ApiClient;

    public HttpFacade(IHttpApiClient apiClient)
    {
        ApiClient = apiClient;
    }
    // HTTP Response
    public async Task<HttpResponseDTOPageQueryResultDTO> GetAllResponses(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.ResponseAsync(caseId, pagingState, pageSize);
    }
    public async Task<HttpResponseDTOPageQueryResultDTO> GetAllResponsesWithSession(Guid caseId, Guid sessionId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.Response2Async(caseId, sessionId, pagingState, pageSize);
    }
    
    // HTTP Request
    public async Task<HttpRequestDTOPageQueryResultDTO> GetAllRequests(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.RequestAsync(caseId, pagingState, pageSize);
    }
    public async Task<HttpRequestDTOPageQueryResultDTO> GetAllRequestsWithSession(Guid caseId, Guid sessionId, byte[] pagingState, int pageSize)
    {
        return await ApiClient.Request2Async(caseId, sessionId, pagingState, pageSize);
    }
}