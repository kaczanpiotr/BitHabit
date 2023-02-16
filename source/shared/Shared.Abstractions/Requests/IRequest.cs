namespace Shared.Abstractions.Requests;

public interface IRequest : IRequest<Task>
{
}

public interface IRequest<T>
{
}

