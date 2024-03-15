namespace PdfTools.Actions
{
    public interface IHttpClient
    {
        byte[] GetPdfAsByteArray(string url);
    }
}