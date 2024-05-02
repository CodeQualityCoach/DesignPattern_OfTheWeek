using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTools.Observer
{
    public abstract class Subject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    public interface IObserver
    {
        void Update(Subject subject);
    }

    public class FileListObserver : IObserver
    {
        private readonly FileCreatedSubject _subject;
        // list of files
        private readonly List<string> _files = new List<string>();

        public FileListObserver(FileCreatedSubject subject)
        {
            _subject = subject;
            _subject.Attach(this);
        }

        public void Update(Subject subject)
        {
            if (subject == _subject)
            {
                // add file to list
                _files.Add(_subject.FileCreatedState);
            }
        }
    }

    internal class FileSizeCounter : IObserver
    {
        private readonly FileCreatedSubject _subject;
        private long _totalSize = 0;

        public FileSizeCounter(FileCreatedSubject subject)
        {
            _subject = subject;
            _subject.Attach(this);
        }

        public void Update(Subject subject)
        {
            if (subject == _subject)
            {
                // get file size
                var fileInfo = new System.IO.FileInfo(_subject.FileCreatedState);
                _totalSize += fileInfo.Length;
            }
        }
    }

    public class FileCreatedSubject : Subject
    {
        public void SetFileCreated(string filename)
        {
            FileCreatedState = filename;
            Notify();
        }
        public string FileCreatedState { get; set; }
    }
}
