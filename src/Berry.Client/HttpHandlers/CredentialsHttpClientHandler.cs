using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.HttpHandlers
{
    public class CredentialsHttpClientHandler : DelegatingHandler
    {
        private readonly string _username;
        private readonly string _password;

        public CredentialsHttpClientHandler(string username, string password)
        {
            _username = username;
            _password = password;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new BasicAuthenticationHeaderValue(_username, _password);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
