namespace MediatorFromSCratch;

class ComponentA : IComponent
{
    private readonly IMediator _mediator;

    public ComponentA(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public void SetState(string state)
    {
    }

    // method DoA(string)
    public void DoA(string message)
    {
        Console.WriteLine("ComponentA does something with state: " + message);
    }
}