namespace PdfTools.Observer
{
    public interface IObserver<in TMessage> : IObserver
    {
        void Handle(TMessage message);
    }

    public interface IObserver // tagging interface
    {
    }

}