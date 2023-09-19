using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades;

public class CaptureFacade : IAppFacade
{
    
    private readonly ICaptureApiClient _apiClient;

    public CaptureFacade(ICaptureApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<CaptureConversationStatisticsListDTOPageQueryResultDTO> GetAll(Guid caseId, byte[] pagingState, int? pageSize)
    {
        return await _apiClient.CaptureAsync(caseId, pagingState, pageSize);
    }

    public async Task<CaptureConversationStatisticsDetailDTO> GetById(Guid caseId, System.Guid captureId)
    {
        return await _apiClient.Capture2Async(caseId, captureId);
    }

    public async Task<CaptureConversationStatisticsDetailDTO> GetByIdAggregated(Guid caseId, System.Guid captureId, System.DateTimeOffset? from, System.DateTimeOffset? to)
    {
        return await _apiClient.AggregateAsync(caseId, captureId, from, to);
    }
}