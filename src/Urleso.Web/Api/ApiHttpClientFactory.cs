using Urleso.Api.Client;

namespace Urleso.Web.Api;

internal sealed class ApiHttpClientFactory(IHttpClientFactory httpClientFactory) : IApiClientFactory
{
    public const string HttpClientName = "api";

    public IApiClient CreateClient()
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        var apiClient = new ApiClient(httpClient);
        return apiClient;
    }
}