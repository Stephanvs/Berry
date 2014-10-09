using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Berry.Client.Test
{
    public class TestHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public TestHandler()
        {
            _handlerFunc = (r, c) => ReturnOk();
        }

        public TestHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }

        public static Task<HttpResponseMessage> ReturnOk()
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK));
        }

        public static Task<HttpResponseMessage> ReturnOk(HttpContent httpContent)
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = httpContent });
        }

        public static Task<HttpResponseMessage> ReturnUnauthorized()
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }

        public static Task<HttpResponseMessage> ReturnUnauthorized(HttpContent httpContent)
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = httpContent });
        }

        public static Task<HttpResponseMessage> ReturnNonAuthoritativeInformation()
        {
            return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.NonAuthoritativeInformation));
        }
    }
}