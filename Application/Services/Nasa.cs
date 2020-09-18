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
        public async Task<string> getDONKIInfo()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    GenerateException();
                    var result = await httpClient.GetAsync("https://api.nasa.gov/DONKI/CMEAnalysis?startDate=2016-09-01&endDate=2016-09-30&mostAccurateOnly=true&speed=500&halfAngle=30&catalog=ALL&api_key=wHFHLf2JGLZwYpki5vreuF9OLzB5KByRRhgN8ELI");
                    if (result.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return JsonSerializer.Serialize(resultContent.ToString());
                });
            }
        }

        public async Task<string> getInSightInfo()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    GenerateException();
                    var result = await httpClient.GetAsync("https://api.nasa.gov/insight_weather/?api_key=wHFHLf2JGLZwYpki5vreuF9OLzB5KByRRhgN8ELI&feedtype=json&ver=1.0");
                    if (result.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return JsonSerializer.Serialize(resultContent.ToString());
                });
            }
        }

        private static void GenerateException()
        {
            Random random = new Random();
            var randomData = random.Next(1, 6);
            switch (randomData)
            {
                case 1: throw new HttpRequestException("Fake request exception");
                case 2: throw new UnauthorizedAccessException("Fake Unauthorized exception");
                case 3: throw new Exception("Fake exception");
            }
        }
    }
}