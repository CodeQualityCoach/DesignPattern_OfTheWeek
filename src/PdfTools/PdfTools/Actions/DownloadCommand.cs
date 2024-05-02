using System;
using System.IO;

namespace PdfTools.Actions
{
    class DownloadCommand : ICommand
    {
        private readonly IHttpClient _httpClient;

        public DownloadCommand(IHttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClientFacade(); // zero impact injection pattern
        }
        public void Execute(string[] args)
        {
            var pdf = _httpClient.GetPdfAsByteArray(args[1]);

            File.WriteAllBytes(args[2], pdf);
        }

        public bool CanExecute(string[] context)
        {
            var action = context[0];
            return string.Equals(action, "download", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}