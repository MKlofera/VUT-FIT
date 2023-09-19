using Pleskalizer.Web.BL.Clients;
using IO.Swagger.Models;

namespace Pleskalizer.Web.BL.Facades;

public class ContentFacade : IAppFacade
{
    private readonly IContentApiClient _apiClient;

    public ContentFacade(IContentApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<ContentSummaryDTO> GetById(Guid id)
    {
        return await _apiClient.SummaryAsync(id);
    }
}