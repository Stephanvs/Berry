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

        
        public OAuthHttpClientHandler(string tokenIdentifier, ITokenStore tokenStore, OAuth2Client oAuth2Client)
        {
            _tokenIdentifier = tokenIdentifier;
            _tokenStore = tokenStore;
            _oAuth2Client = oAuth2Client;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenCredential = _tokenStore.GetToken(_tokenIdentifier);
            AssignAuthenticationHeader(request, tokenCredential);
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
            var tokenCredential = _tokenStore.GetToken(_tokenIdentifier);

            // AccessToken is expired -> try to refresh
            var tokenResponse = await _oAuth2Client.RequestRefreshTokenAsync(tokenCredential.RefreshToken);

            if (tokenResponse.IsError)
            {
                throw new InvalidRefreshTokenException(tokenResponse);
            }

            _tokenStore.StoreToken(_tokenIdentifier, tokenResponse);
            tokenCredential = _tokenStore.GetToken(_tokenIdentifier);

            return tokenCredential;
        }
    }
}