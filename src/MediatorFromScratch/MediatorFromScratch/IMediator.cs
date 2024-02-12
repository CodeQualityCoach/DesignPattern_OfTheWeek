namespace MediatorFromSCratch;

public interface IMediator
{
    void DoA(string message);
    string GetB();
    void SetState(string state);
}