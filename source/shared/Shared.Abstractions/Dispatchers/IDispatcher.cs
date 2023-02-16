using Shared.Abstractions.Events;
using Shared.Abstractions.Requests;

namespace Shared.Abstractions.Dispatchers;

public interface IDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class;
    Task Notify<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
}