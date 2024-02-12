namespace MediatorFromSCratch;

class Mediator : IMediator
{
    private readonly ComponentA _componentA;
    private readonly ComponentB _componentB;

    public Mediator()
    {
        _componentA = new ComponentA(this);
        _componentB = new ComponentB(this);
    }
    public void DoA(string message)
    {
        _componentA.DoA(message);
    }

    public string GetB()
    {
        return _componentB.GetB();
    }

    public void SetState(string state)
    {
        _componentA.SetState(state);
        _componentB.SetState(state);
    }
}