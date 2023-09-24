using MediatR;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;
using UPS.Application.Core.Interfaces;
using UPS.Application.Users.Mapping;

namespace UPS.Application.Users.Queries;

public sealed record GetUserPagedListQuery : IRequest<Result<UserPagedListResponse>>
{
    public int Page { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Status { get; set; }
}

public sealed class GetUserPagedListQueryHandler : IRequestHandler<GetUserPagedListQuery, Result<UserPagedListResponse>>
{
    private readonly IUserService _userService;

    public GetUserPagedListQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<UserPagedListResponse>> Handle(GetUserPagedListQuery request,
        CancellationToken cancellationToken)
    {
        var query = request.FromCommand();

        return await _userService.GetAllUsersAsync(query, request.Page, cancellationToken);
    }
}