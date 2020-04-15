using System;
using System.Net.Http;

namespace AppCoreSample.Util
{
    public static class HttpClientHelper
    {
        public static string GetByHttpClient(string url) {
            using (HttpClient httpClient=new HttpClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(httpRequestMessage).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
