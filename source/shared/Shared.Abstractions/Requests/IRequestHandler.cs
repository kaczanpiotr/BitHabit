namespace Shared.Abstractions.Requests;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Task> where TRequest : class, IRequest<Task>
{

}
