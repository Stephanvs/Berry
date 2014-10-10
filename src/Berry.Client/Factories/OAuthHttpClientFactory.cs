using System;
using System.Net.Http;
using Berry.Client.HttpHandlers;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.Factories
{
    public class OAuthHttpClientFactory : IOAuthHttpClientFactory
    {
        public HttpClient CreateClient(string baseAddress, string tokenIdentifier, ITokenStore tokenStore, OAuth2Client oAuth2Client)
        {
            var client = HttpClientFactory.Create(new HttpClientHandler(), new OAuthHttpClientHandler(tokenIdentifier, tokenStore, oAuth2Client));
            client.BaseAddress = new Uri(baseAddress);
            return client;
        }
    }
}