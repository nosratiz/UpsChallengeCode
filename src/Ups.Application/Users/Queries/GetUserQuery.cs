using MediatR;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;
using UPS.Application.Core.Interfaces;

namespace UPS.Application.Users.Queries;

public sealed record GetUserQuery(long Id) : IRequest<Result<UserResponse>>;


public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery,Result<UserResponse>>
{
    private readonly IUserService _userService;

    public GetUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserAsync(request.Id, cancellationToken);
    }
}