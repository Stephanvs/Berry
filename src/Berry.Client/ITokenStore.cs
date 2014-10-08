using Thinktecture.IdentityModel.Client;

namespace Berry.Client
{
    public interface ITokenStore
    {
        void StoreToken(string identifier, TokenResponse response);
        void StoreToken(string identifier, AuthorizeResponse response);
        bool TryGetToken(string resourceName, out TokenCredential tokenCredential);
        void RemoveToken(string identifier);
    }
}