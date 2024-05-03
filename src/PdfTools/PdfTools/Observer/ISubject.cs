namespace PdfTools.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Publish<TMessage>(TMessage message);
    }
}