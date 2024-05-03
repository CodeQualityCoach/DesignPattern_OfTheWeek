using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf;
using PdfTools.Actions;
using PdfTools.Logging;
using PdfTools.Logging.NLog;
using PdfTools.Observer;
using QRCoder;
using Image = iTextSharp.text.Image;

namespace PdfTools
{
    public class Program
    {
        private static IPdfToolsLogger _logger;

        public static void Main(string[] args)
        {
            _logger = new LoggerFactory().CreateLogger(typeof(Program));

#if DEBUG
            // just a hack in case you hit play in VS
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Args (Comma Separated): ");
                var arg = Console.ReadLine() ?? "help";
                args = arg.Split(',').Select(x => x.Trim()).ToArray();

                // archive,http://www.lug.or.kr/files/cheat_sheet/design_pattern_cheatsheet_v1.pdf,design_pattern_cheatsheet_v1.pdf
            }
#endif

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            if (args.Length == 0)
                throw new ArgumentException("at least an command is required");

            var action = args[0];

            var subject = new Subject();
            _ = new HistoryLogObserver(subject);
            _ = new FileSizeCounter(subject);

            // ==============
            IList<ICommand> myCommand = new List<ICommand>();

            myCommand.Add(new EmptyDecoratorCommand(new LoggingDecorator(new CreateCommand(), _logger)));
            myCommand.Add(new AddCodeCommand());
            myCommand.Add(new ArchiveCommand(subject));
            myCommand.Add(new CombineCommand());
            myCommand.Add(new DownloadCommand());
            myCommand.Add(new LogArgs());

            DoMain(myCommand, args);

#if DEBUG
            Console.ReadKey();
#endif
        }

        public static void DoMain(IEnumerable<ICommand> commands, string[] args)
        {
            foreach (var command in commands)
            {
                if (!command.CanExecute(args)) continue;
                command.Execute(args);
            }
        }
    }

























    public class PdfArchiver
    {
        private readonly IHttpClient _httpClient;
        private readonly string _tempFile;
        private readonly ICodeGenerator _codeGenerator;

        public PdfArchiver(ICodeGenerator codeGenerator = null, IHttpClient httpClient = null, ISubject subject = null)
        {
            _httpClient = httpClient ?? new HttpClientFacade();
            _tempFile = Path.GetTempFileName();
            subject?.Publish(new FileCreatedMessage() { FileName = _tempFile });
            _codeGenerator = codeGenerator ?? new QrCodeGenerator();
        }

        public void Archive(string url)
        {
            var pdf = _httpClient.GetPdfAsByteArray(url);

            var tmpTempFile = Path.GetTempFileName();
            File.WriteAllBytes(tmpTempFile, pdf);

            using (Stream inputPdfStream = new FileStream(tmpTempFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new MemoryStream())
            using (Stream outputPdfStream = new FileStream(_tempFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var code = _codeGenerator.CreateInitCode(url);
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

        public void SaveAs(string destFile)
        {
            File.Copy(_tempFile, destFile, true);
        }
    }

    public interface ICodeGenerator
    {
        Bitmap CreateInitCode(string text);
    }

    public class QrCodeGenerator : ICodeGenerator
    {
        public Bitmap CreateInitCode(string text)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCodeData = qrCodeGenerator.CreateQrCode(new PayloadGenerator.Url(text), QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(2);
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
