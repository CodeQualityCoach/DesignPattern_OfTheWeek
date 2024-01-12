using System;
using System.IO;
using System.Net.Http;

namespace PdfTools.Actions
{
    class DownloadCommand : ICommand
    {
        public void Execute(string[] args)
        {
            var client = new HttpClient();
            var response = client.GetAsync(args[1]).Result;
            var pdf = response.Content.ReadAsByteArrayAsync().Result;

            File.WriteAllBytes(args[2], pdf);
        }

        public bool CanExecute(string[] context)
        {
            var action = context[0];
            return string.Equals(action, "download", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}