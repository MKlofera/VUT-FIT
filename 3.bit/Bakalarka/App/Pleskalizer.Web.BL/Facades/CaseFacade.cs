using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades;

public class CaseFacade : IAppFacade
{
    private readonly ICaseApiClient _apiClient;

    public CaseFacade(ICaseApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<CaseListDTOPageQueryResultDTO> GetAll(byte[] pagingState, int? pageSize)
    {
        return await _apiClient.CaseGETAsync(pagingState, pageSize);
    }

    public async Task<CaseDetailDTO> GetById(Guid id)
    {
        return await _apiClient.CaseGET2Async(id);
    }

}