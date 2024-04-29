using MediatR;

namespace BlazingBlog.Application.Abstractions.RequestHandling
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
