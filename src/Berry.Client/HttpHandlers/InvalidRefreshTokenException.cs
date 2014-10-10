using System;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.HttpHandlers
{
    public class InvalidRefreshTokenException : Exception
    {
        public TokenResponse TokenResponse { get; set; }

        public InvalidRefreshTokenException(TokenResponse tokenResponse)
        {
            TokenResponse = tokenResponse;
        }
    }
}