using System.IO;
using System.Net.Http;

namespace PdfTools.Actions
{
    class DownloadAction : IAction
    {
        public void Do(string[] args)
        {
            var client = new HttpClient();
            var response = client.GetAsync(args[1]).Result;
            var pdf = response.Content.ReadAsByteArrayAsync().Result;

            File.WriteAllBytes(args[2], pdf);
        }

        public void GetHelp()
        {
            throw new System.NotImplementedException();
        }
    }
}