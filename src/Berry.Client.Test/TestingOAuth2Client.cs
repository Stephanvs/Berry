using System;
using System.Net.Http;
using Newtonsoft.Json;
using Thinktecture.IdentityModel.Client;

namespace Berry.Client.Test
{
    public class TestingOAuth2Client : OAuth2Client
    {
        public TestingOAuth2Client(string accessToken, string refreshToken)
            : base(new Uri("http://test/"), new TestHandler((r, c) => TestHandler.ReturnOk(new StringContent(JsonConvert.SerializeObject(new { access_token = accessToken,
                                                                                                                                               refresh_token = refreshToken })))))
        {
        }

        public TestingOAuth2Client(string error)
            : base(new Uri("http://test/"), new TestHandler((r, c) => TestHandler.ReturnUnauthorized(new StringContent(JsonConvert.SerializeObject(new { error })))))
        {
        }
    }
}