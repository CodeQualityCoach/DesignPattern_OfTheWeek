using System;
using System.Diagnostics;

namespace CastleItWillBe.Models
{
    public class SendMailServices
    {

        public interface ISendMailService
        {
            string SendMail(string recipient, string message);
        }

        public class BlockConfidentialMailsDecorator : ISendMailService
        {
            private readonly ISendMailService _sendMailService;

            public BlockConfidentialMailsDecorator(ISendMailService sendMailService)
            {
                _sendMailService = sendMailService;
            }

            public string SendMail(string recipient, string message)
            {
                if (message.Contains("confidential"))
                {
                    throw new ArgumentException("cannot send confidential mails");
                }

                return _sendMailService.SendMail(recipient, message);
            }
        }

        public class AwesomeMailsDecorator : ISendMailService
        {
            private readonly ISendMailService _sendMailService;

            public AwesomeMailsDecorator(ISendMailService sendMailService)
            {
                _sendMailService = sendMailService;
            }

            public string SendMail(string recipient, string message)
            {
                return _sendMailService.SendMail(recipient, $"Awesome: {message}");
            }
        }

        public class SendMailService : ISendMailService
        {
            public SendMailService(IMailStrategy mailStrategy, IDemoLogger logger)
            {
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{mailStrategy.GetType().Name}\t{mailStrategy.GetHashCode()}");
                Debug.WriteLine($"\t\t{mailStrategy.Logger.GetType().Name}\t{mailStrategy.Logger.GetHashCode()}");
                Debug.WriteLine($"\t{logger.GetType().Name}\t{logger.GetHashCode()}");
            }

            public string SendMail(string recipient, string message)
            {
                Debugger.Break();

                return "email sent";
            }
        }

        public class EmptyMailService : ISendMailService
        {
            public EmptyMailService()
            {
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
            }

            public string SendMail(string recipient, string message)
            {
                return string.Empty;
            }
        }
    }
}