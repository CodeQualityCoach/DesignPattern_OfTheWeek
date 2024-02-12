namespace MediatorFromSCratch;

class ComponentB : IComponent
{
    private readonly IMediator _mediator;

    public ComponentB(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    public void SetState(string state)
    {

    }

    public string GetB()
    {
        return "this is b";
    }
}