using Urleso.Api.Client;

namespace Urleso.Web.Api;

public interface IApiClientFactory
{
    public IApiClient CreateClient();
}