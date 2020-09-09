using Demos.API.Application.Contracts;
using Polly;
using Polly.Retry;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demos.API.Application.Services
{
    public class Nasa : INasa
    {
        private const int MaxRetries = 3;
        private readonly AsyncRetryPolicy<string> _retryPolicy;
        public Nasa()
        {
            //default
            //_retryPolicy = Policy<string>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
            //Wait And Retry  
            /*_retryPolicy = Policy<string>.Handle<HttpRequestException>(exception =>
            {
                return exception.Message != "Fake request exception";
            }).WaitAndRetryAsync(MaxRetries, times =>
            TimeSpan.FromMilliseconds(times * 100));*/
            _retryPolicy = Policy<string>.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, times =>
                TimeSpan.FromMilliseconds(times * 100));
        }
        public async Task<string> getDONKIAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Random random = new Random();
                return await _retryPolicy.ExecuteAsync(async () =>
                   {
                       if (random.Next(1, 3) == 1)
                           throw new HttpRequestException("Fake request exception");
                       var commicResult = await httpClient.GetAsync("https://api.nasa.gov/DONKI/CMEAnalysis?startDate=2016-09-01&endDate=2016-09-30&mostAccurateOnly=true&speed=500&halfAngle=30&catalog=ALL&api_key=wHFHLf2JGLZwYpki5vreuF9OLzB5KByRRhgN8ELI");
                       if (commicResult.StatusCode == HttpStatusCode.NotFound)
                           return null;
                       var resultContent = await commicResult.Content.ReadAsStringAsync();
                       return JsonSerializer.Serialize(resultContent.ToString());
                   });
            }
        }
    }
}