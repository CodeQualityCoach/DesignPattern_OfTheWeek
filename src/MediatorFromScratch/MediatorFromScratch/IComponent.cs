namespace MediatorFromSCratch;

public interface IComponent
{
    // setstate Methode
    void SetState(string state);
}

class AwesomeComponent : IComponent
{
    private readonly IMediator _mediator;

    public AwesomeComponent(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public void SetState(string state)
    {
        
    }

    public void DoAwesomeStuff()
    {
        var newState = _mediator.GetB();
        _mediator.SetState(newState);
    }
}