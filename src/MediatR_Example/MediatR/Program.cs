using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRSchleupen
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddMediatR(c =>
                {
                    c.RegisterServicesFromAssemblyContaining<Program>();
                });

            var container = services.BuildServiceProvider();

            var mediator = container.GetRequiredService<IMediator>();

            var pong = await mediator.Send(new Ping() { Message = "Hello" });

            await mediator.Publish(new ChangeState() {State = "open"});
        }
    }

    public class ChangeState : INotification
    {
        public string State { get; set; }
    }

    public class ChangeStateHandler : INotificationHandler<ChangeState>
    {
        public Task Handle(ChangeState notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"State changed to {notification.State}");
            return Task.CompletedTask;
        }
    }

    public class AwesomeChangeStateHandler : INotificationHandler<ChangeState>
    {
        public Task Handle(ChangeState notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"State changed awesomely to {notification.State}");
            return Task.CompletedTask;
        }
    }

    public class Pong
    {
        public string Message { get; set; }
    }

    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }

    public class AwesomePingPingRequestHandler : IRequestHandler<Ping, Pong>
    {
        public Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Pong() { Message = request.Message + " Awesome Pong" });
        }
    }

    public class PingPingRequestHandler : IRequestHandler<Ping, Pong>
    {
        public Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Pong() { Message = request.Message + " Pong" });
        }
    }
}
