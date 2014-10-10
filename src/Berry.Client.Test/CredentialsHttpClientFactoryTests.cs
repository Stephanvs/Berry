using System.Net.Http;
using System.Threading.Tasks;
using Berry.Client.Factories;
using Berry.Client.HttpHandlers;
using FluentAssertions;
using Xunit;

namespace Berry.Client.Test
{
    public class CredentialsHttpClientFactoryTests
    {
        [Fact]
        public void ShouldSetBaseAddress()
        {
            // Arrange
            var factory = new CredentialsHttpClientFactory();
            
            // Act
            var httpClient = factory.CreateClient("http://test/", "username", "password");

            // Assert
            httpClient.BaseAddress.Should().Be("http://test/");
        }

        [Fact]
        public async Task ShouldSetCredentials()
        {
            // Arrange
            var handler = new CredentialsHttpClientHandler("username", "password") { InnerHandler = new TestHandler((r, c) => TestHandler.ReturnOk()) };
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://test/");
            var httpClient = new HttpClient(handler);

            // Act
            await httpClient.SendAsync(httpRequestMessage);

            // Assert
            httpRequestMessage.Headers.Authorization.Should().Be(new Thinktecture.IdentityModel.Client.BasicAuthenticationHeaderValue("username", "password"));
        }
    }
}
