namespace PdfTools.Observer
{
    internal class FileSizeCounter : IObserver<FileCreatedMessage>
    {
        private long _totalSize = 0;

        public FileSizeCounter(Subject subject)
        {
            subject.Attach(this);
        }

        public void Handle(FileCreatedMessage message)
        {
            // get file size
            var fileInfo = new System.IO.FileInfo(message.FileName);
            _totalSize += fileInfo.Length;
        }
    }
}