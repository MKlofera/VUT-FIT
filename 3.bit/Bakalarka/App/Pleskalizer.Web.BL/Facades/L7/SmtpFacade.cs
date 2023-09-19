using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades.L7;

public class SmtpFacade : IAppFacade
{
    private readonly ISmtpApiClient _apiClient;

    public SmtpFacade(ISmtpApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    // SMTP client message
    public async Task<SmtpClientMessageDTOPageQueryResultDTO> GetAllClientMessages(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await _apiClient.ClientMessage3Async(caseId, pagingState, pageSize);
    }
    
    public async Task<SmtpClientMessageDTOPageQueryResultDTO> GetAllClientMessagesWithSession(Guid caseId, Guid sessionId, byte[] pagingState, int pageSize)
    {
        return await _apiClient.ClientMessage4Async(caseId, sessionId, pagingState, pageSize);
    }
    
    
    // SMTP server message
    public async Task<SmtpServerMessageDTOPageQueryResultDTO> GetAllServerMessages(Guid caseId, byte[] pagingState, int pageSize)
    {
        return await _apiClient.ServerMessage3Async(caseId, pagingState, pageSize);
    }
    
    public async Task<SmtpServerMessageDTOPageQueryResultDTO> GetAllServerMessagesWithSession(Guid caseId, Guid sessionId, byte[] pagingState, int pageSize)
    {
        return await _apiClient.ServerMessage4Async(caseId, sessionId, pagingState, pageSize);
    }
}