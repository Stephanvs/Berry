using Thinktecture.IdentityModel.Client;

namespace Berry.Client
{
    public interface ITokenStore
    {
        void StoreToken(string identifier, TokenResponse response);
        void StoreToken(string identifier, AuthorizeResponse response);
        TokenCredential GetToken(string resourceName);
        void RemoveToken(string identifier);
    }
}