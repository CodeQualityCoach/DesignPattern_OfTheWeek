using System.Collections.Generic;

namespace PdfTools.Observer
{
    public class HistoryLogObserver : IObserver<FileCreatedMessage>, IObserver<WebRequestExecutedMessage>
    {
        private readonly List<string> _history = new List<string>();

        public HistoryLogObserver(Subject subject)
        {
            subject.Attach(this);
        }

        public void Handle(FileCreatedMessage message)
        {
            _history.Add(message.FileName);
        }

        public void Handle(WebRequestExecutedMessage message)
        {
            _history.Add(message.Url);
        }
    }
}