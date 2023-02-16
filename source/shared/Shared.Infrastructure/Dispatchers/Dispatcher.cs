using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Dispatchers;
using Shared.Abstractions.Events;
using Shared.Abstractions.Requests;

namespace Shared.Infrastructure.Dispatchers;

internal class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle));

        if (method is null)
            throw new InvalidOperationException($"Query handler for '{typeof(TResponse).Name}' is invalid.");

        return await (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken });
    }

    public async Task Notify<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }
}

