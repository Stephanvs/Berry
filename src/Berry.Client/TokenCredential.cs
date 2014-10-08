using System;

namespace Berry.Client
{
    public sealed class TokenCredential
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public DateTime Expires { get; set; }

        public bool IsValid
        {
            get
            {
                return Expires.Subtract(TimeSpan.FromSeconds(30)) > DateTime.UtcNow;
            }
        }
    }
}