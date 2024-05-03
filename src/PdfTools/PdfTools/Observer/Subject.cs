using System.Collections.Generic;
using System.Linq;

namespace PdfTools.Observer
{
    public class Subject : ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Publish<TMessage>(TMessage message)
        {
            var handler = _observers.OfType<IObserver<TMessage>>();
            foreach (var observer in handler)
            {
                observer.Handle(message);
            }
        }
    }
}