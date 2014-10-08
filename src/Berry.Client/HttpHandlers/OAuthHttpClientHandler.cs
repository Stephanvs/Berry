using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.HttpHandlers
{
    public class OAuthHttpClientHandler : DelegatingHandler
    {
        private readonly string _tokenIdentifier;
        private readonly ITokenStore _tokenStore;
        private readonly OAuth2Client _oAuth2Client;
        private readonly IInvalidTokenHandler _invalidTokenHandler;

        public OAuthHttpClientHandler(string tokenIdentifier, ITokenStore tokenStore, OAuth2Client oAuth2Client, IInvalidTokenHandler invalidTokenHandler)
        {
            _tokenIdentifier = tokenIdentifier;
            _tokenStore = tokenStore;
            _oAuth2Client = oAuth2Client;
            _invalidTokenHandler = invalidTokenHandler;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            TokenCredential tokenCredential;
            if (_tokenStore.TryGetToken(_tokenIdentifier, out tokenCredential))
            {
                AssignAuthenticationHeader(request, tokenCredential);
            }

            var response = await base.SendAsync(request, cancellationToken);

            return await PreHandleResponseMessage(response, request, cancellationToken);
        }

        private void AssignAuthenticationHeader(HttpRequestMessage request, TokenCredential tokenCredential)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenCredential.AccessToken);
        }

        private async Task<HttpResponseMessage> PreHandleResponseMessage(HttpResponseMessage response, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.NonAuthoritativeInformation)
            {
                var tokenCredential = await RefreshAndStoreToken();
                if (tokenCredential != null)
                {
                    AssignAuthenticationHeader(request, tokenCredential);

                    // Re-issue previous request and pass along response
                    return await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }

        protected async Task<TokenCredential> RefreshAndStoreToken()
        {
            TokenCredential tokenCredential;
            if (_tokenStore.TryGetToken(_tokenIdentifier, out tokenCredential))
            {
                // AccessToken is expired -> try to refresh
                //var oAuth2Service = new OAuth2Client(new Uri(_tokenEndPoint), "roclient", "secret");
                var tokenResponse = await _oAuth2Client.RequestRefreshTokenAsync(tokenCredential.RefreshToken);

                if (tokenResponse.IsError)
                {
                    tokenCredential = null;
                    _invalidTokenHandler.Invoke();
                }
                else
                {
                    _tokenStore.StoreToken(_tokenIdentifier, tokenResponse);
                    _tokenStore.TryGetToken(_tokenIdentifier, out tokenCredential);
                }
            }

            return tokenCredential;
        }
    }
}