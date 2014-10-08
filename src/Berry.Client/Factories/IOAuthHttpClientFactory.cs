using System.Net.Http;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.Factories
{
    public interface IOAuthHttpClientFactory
    {
        HttpClient CreateClient(string baseAddress, string tokenIdentifier, ITokenStore tokenStore, OAuth2Client oAuth2Client, IInvalidTokenHandler invalidTokenHandler);
    }
}