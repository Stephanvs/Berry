using System;
using System.Net.Http;
using Berry.Client.HttpHandlers;

namespace Berry.Client.Factories
{
    public class CredentialsHttpClientFactory : ICredentialsHttpClientFactory
    {
        public HttpClient CreateClient(string baseAddress, string username, string password)
        {
            var client = HttpClientFactory.Create(new HttpClientHandler(), new CredentialsHttpClientHandler(username, password));
            client.BaseAddress = new Uri(baseAddress);
            return client;
        }
    }
}