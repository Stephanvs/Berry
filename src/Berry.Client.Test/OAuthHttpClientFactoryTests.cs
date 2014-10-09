using System;
using Berry.Client.Factories;
using FakeItEasy;
using FluentAssertions;
using Thinktecture.IdentityModel.Client;
using Xunit;

namespace Berry.Client.Test
{
    public class OAuthHttpClientFactoryTests
    {
        [Fact]
        public void ShouldSetBaseAddress()
        {
            // Arrange
            var factory = new OAuthHttpClientFactory();
            var oAuth2Client = new OAuth2Client(new Uri("http://test/oauth"));
            var tokenStore = A.Fake<ITokenStore>();

            // Act
            var httpClient = factory.CreateClient("http://test/", "test", tokenStore, oAuth2Client);

            // Assert
            httpClient.BaseAddress.Should().Be("http://test/");
        }
    }
}