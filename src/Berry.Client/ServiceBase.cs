using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Berry.Client
{
    public abstract class ServiceBase
    {
        protected abstract HttpClient CreateClient();

        protected virtual async Task<T> HandleResponseMessageAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await HandleSuccessResponseMessage<T>(response);
            }

            return await HandleErrorResponse<T>(response);
        }

        protected virtual async Task<T> HandleSuccessResponseMessage<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected virtual async Task<T> HandleErrorResponse<T>(HttpResponseMessage response)
        {
            return await Task.FromResult(default(T));
        }

        protected virtual async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (var client = CreateClient())
            {
                return await client.GetAsync(url);
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent body)
        {
            using (var client = CreateClient())
            {
                return await client.PostAsync(url, body);
            }
        }
    }
}