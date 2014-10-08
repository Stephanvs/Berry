using System.Net.Http;

namespace Berry.Client.Factories
{
    public interface ICredentialsHttpClientFactory
    {
        HttpClient CreateClient(string baseAddress, string username, string password);
    }
}
