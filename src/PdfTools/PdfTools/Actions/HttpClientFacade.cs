using System.Net.Http;

namespace PdfTools.Actions
{
    class HttpClientFacade : IHttpClient
    {
        public HttpClientFacade(HttpClient client)
        {
            
        }

        public byte[] GetPdfAsByteArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var pdf = response.Content.ReadAsByteArrayAsync().Result;
            return pdf;
        }
    }
}