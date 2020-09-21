using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demos.API.Application.Services
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClientFactory;

        public CustomHttpClient(HttpClient httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> getInSightInfoTyped()
        {
            var result = await _httpClientFactory.GetAsync("");
            if (result.StatusCode == HttpStatusCode.NotFound)
                return null;
            var resultContent = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Serialize(resultContent.ToString());
        }
    }
}
