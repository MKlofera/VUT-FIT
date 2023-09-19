using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L7;

public class PopFacade : IAppFacade
{
    public readonly IPopApiClient ApiClient;

    public PopFacade(IPopApiClient apiClient)
    {
        ApiClient = apiClient;
    }
    
    // Client messages 
    public async Task<PopClientMessageDTOPageQueryResultDTO> GetAllClientMessages(Guid caseId, byte[] pagingState, int? pageSize)
    {
        return await ApiClient.ClientMessageAsync(caseId, pagingState, pageSize);
    }

    public async Task<PopClientMessageDTOPageQueryResultDTO> GetSessionClientMessages(Guid caseId, Guid sessionId, byte[] pagingState, int? pageSize)
    {
        return await ApiClient.ClientMessage2Async(caseId, sessionId, pagingState, pageSize);
    }
    
    //Server messages
    public async Task<PopServerMessageDTOPageQueryResultDTO> GetAllServerMessages(Guid caseId, byte[] pagingState, int? pageSize)
    {
        return await ApiClient.ServerMessageAsync(caseId, pagingState, pageSize);
    }

    public async Task<PopServerMessageDTOPageQueryResultDTO> GetSessionServerMessages(Guid caseId, Guid sessionId, byte[] pagingState, int? pageSize)
    {
        return await ApiClient.ServerMessage2Async(caseId, sessionId, pagingState, pageSize);
    }
}