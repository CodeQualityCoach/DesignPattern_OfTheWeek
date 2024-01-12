using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using FSharp.Markdown;
using FSharp.Markdown.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NLog;
using PdfTools.Actions;
using QRCoder;
using Image = iTextSharp.text.Image;

namespace PdfTools
{
    public class Program
    {
        private static Logger _logger;

        public static void Main(string[] args)
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();

#if DEBUG
            // just a hack in case you hit play in VS
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Args (Comma Separated): ");
                var arg = Console.ReadLine() ?? "help";
                args = arg.Split(',').Select(x => x.Trim()).ToArray();
            }
#endif

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            var action = args[0];
            IAction myAction = new EmptyAction();

            // markdown-in, pdf-out
            if (string.Equals(action, "create", StringComparison.CurrentCultureIgnoreCase))
                myAction = new CreateAction();

            // pdf-in, qrcodetext, optional outfile
            if (string.Equals(action, "addcode", StringComparison.CurrentCultureIgnoreCase))
            {
                myAction = new AddCodeAction();
            }

            // url, outfile
            if (string.Equals(action, "download", StringComparison.CurrentCultureIgnoreCase))
            {
                myAction = new DownloadAction();
            }

            // url, outfile
            if (string.Equals(action, "archive", StringComparison.CurrentCultureIgnoreCase))
            {
                myAction = new ArchiveAction();
            }

            // url, outfile
            if (string.Equals(action, "combine", StringComparison.CurrentCultureIgnoreCase))
            {
                myAction = new CombineAction();
            }

            DoMain(myAction, args);

#if DEBUG
            Console.ReadKey();
#endif
        }

        public static void DoMain(IAction action, string[] args)
        {
            if (args.Length < 2) { action.GetHelp(); }

            action.Do(args);
        }

    }

























    public class PdfArchiver
    {
        private readonly string _tempFile;

        public PdfArchiver()
        {
            _tempFile = Path.GetTempFileName();
        }
        public void Archive(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var pdf = response.Content.ReadAsByteArrayAsync().Result;

            var tmpTempFile = Path.GetTempFileName();
            File.WriteAllBytes(tmpTempFile, pdf);

            using (Stream inputPdfStream = new FileStream(tmpTempFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new MemoryStream())
            using (Stream outputPdfStream = new FileStream(_tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var code = CreateInitCode(url);
                code.Save(inputImageStream, ImageFormat.Jpeg);
                inputImageStream.Position = 0;

                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                var image = Image.GetInstance(inputImageStream);
                image.SetAbsolutePosition(5, 5);
                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }

        private Bitmap CreateInitCode(string text)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(new PayloadGenerator.Url(text), QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(2);
        }

        public void SaveAs(string destFile)
        {
            File.Copy(_tempFile, destFile, true);
        }
    }

    public class PdfCodeEnhancer
    {
        private readonly string _pdfFile;
        private readonly string _tempFile;

        public PdfCodeEnhancer(string pdfFile)
        {
            _pdfFile = pdfFile;
            _tempFile = Path.GetTempFileName();
        }

        public void AddTextAsCode(string text)
        {
            using (Stream inputPdfStream = new FileStream(_pdfFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new MemoryStream())
            using (Stream outputPdfStream = new FileStream(_tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var code = CreateInitCode(text);
                code.Save(inputImageStream, ImageFormat.Jpeg);
                inputImageStream.Position = 0;

                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                var image = Image.GetInstance(inputImageStream);
                image.SetAbsolutePosition(5, 5);
                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }

        private Bitmap CreateInitCode(string text)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(2);
        }

        public void SaveAs(string destFile)
        {
            File.Copy(_tempFile, destFile, true);
        }
    }
}
