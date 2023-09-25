using MediatR;
using Moq;
using Ups.App;

namespace Ups.UnitTest;

public class BaseConfiguration
{
    private IMediator _mediator;

    public BaseConfiguration()
    {
        _mediator = new Mock<IMediator>().Object;
    }

    public BaseConfiguration(IMediator mediator)
    {
        _mediator = mediator;
    }

    internal BaseConfiguration WithMediatorService(IMediator mediator)
    {
        _mediator = mediator;
        return this;
    }

    internal async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request, cancellationToken);
    }



 
}