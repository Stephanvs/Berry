using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Berry.Client.HttpHandlers;
using FakeItEasy;
using FluentAssertions;
using Thinktecture.IdentityModel.Client;
using Xunit;

namespace Berry.Client.Test
{
    public class OAuthHttpClientHandlerTests
    {
        [Fact]
        public async Task ShouldSetBearerToken()
        {
            // Arrange
            var tokenStore = A.Fake<ITokenStore>();
            A.CallTo(() => tokenStore.GetToken("tokenid")).Returns(new TokenCredential { AccessToken = "access_token" });
            var handler = new OAuthHttpClientHandler("tokenid", tokenStore, null) { InnerHandler = new TestHandler((r, c) => TestHandler.ReturnOk()) };
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test/");
            var client = new HttpClient(handler);
            // Act

            await client.SendAsync(httpRequestMessage);

            // Assert
            httpRequestMessage.Headers.Authorization.Should().Be(new AuthenticationHeaderValue("Bearer", "access_token"));
        }

        [Fact]
        public async Task ShouldNotSetBearerToken()
        {
            // Arrange
            var tokenStore = A.Fake<ITokenStore>();
            var handler = new OAuthHttpClientHandler("tokenid", tokenStore, null) { InnerHandler = new TestHandler((r, c) => TestHandler.ReturnOk()) };
            var client = new HttpClient(handler);

            // Act
            await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://test/"));

            // Assert
            A.CallTo(() => tokenStore.GetToken("tokenid")).Throws(new KeyNotFoundException());
        }

        [Fact]
        public async Task ShouldRefreshTokenWhenUnauthorized()
        {
            await ShouldRefreshToken((r, c) => TestHandler.ReturnUnauthorized());
        }

        [Fact]
        public async Task ShouldRefreshTokenWhenNonAuthoritativeInformation()
        {
            await ShouldRefreshToken((r, c) => TestHandler.ReturnNonAuthoritativeInformation());
        }

        private async Task ShouldRefreshToken(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            // Arrange
            var tokenStore = A.Fake<ITokenStore>();
            var tokenCredential = new TokenCredential { AccessToken = "access_token", RefreshToken = "refresh_token" };
            A.CallTo(() => tokenStore.GetToken("tokenid")).ReturnsLazily(() => tokenCredential);
            A.CallTo(() => tokenStore.StoreToken("tokenid", A<TokenResponse>._)).Invokes(x => { tokenCredential.AccessToken = x.Arguments.Get<TokenResponse>("response").AccessToken; });
            var timesSendAsync = 0;
            var oAuth2Client = new TestingOAuth2Client("new_access_token", "new_refresh_token");
            var handler = new OAuthHttpClientHandler("tokenid", tokenStore, oAuth2Client) { InnerHandler = new TestHandler((r, c) => { timesSendAsync++; return handlerFunc.Invoke(r, c); }) };
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test/");
            var client = new HttpClient(handler);

            // Act
            await client.SendAsync(httpRequestMessage);

            // Assert 
            A.CallTo(() => tokenStore.StoreToken("tokenid", A<TokenResponse>.That.Matches(x => x.AccessToken == "new_access_token"))).MustHaveHappened(Repeated.Exactly.Once);
            httpRequestMessage.Headers.Authorization.Should().Be(new AuthenticationHeaderValue("Bearer", "new_access_token"));
            timesSendAsync.Should().Be(2);
        }

        [Fact]
        public void ShouldNotRefreshTokenWhenUnauthorized()
        {
            ShouldNotRefreshToken((r, c) => TestHandler.ReturnUnauthorized());
        }

        [Fact]
        public void ShouldNotRefreshTokenWhenNonAuthoritativeInformation()
        {
            ShouldNotRefreshToken((r, c) => TestHandler.ReturnNonAuthoritativeInformation());
        }

        private void ShouldNotRefreshToken(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            // Arrange
            var tokenStore = A.Fake<ITokenStore>();
            var tokenCredential = new TokenCredential { AccessToken = "access_token", RefreshToken = "refresh_token" };
            A.CallTo(() => tokenStore.GetToken("tokenid")).ReturnsLazily(() => tokenCredential);
            A.CallTo(() => tokenStore.StoreToken("tokenid", A<TokenResponse>._)).Invokes(x => { tokenCredential.AccessToken = x.Arguments.Get<TokenResponse>("response").AccessToken; });
            var oAuth2Client = new TestingOAuth2Client("error");
            var handler = new OAuthHttpClientHandler("tokenid", tokenStore, oAuth2Client) { InnerHandler = new TestHandler(handlerFunc) };
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test/");
            var client = new HttpClient(handler);

            // Act/Assert
            Assert.ThrowsAsync<InvalidRefreshTokenException>(() => client.SendAsync(httpRequestMessage));
        }
    }
}