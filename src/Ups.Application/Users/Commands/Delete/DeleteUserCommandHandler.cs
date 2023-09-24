using MediatR;
using UPS.Application.Core.Contracts.Requests;
using UPS.Application.Core.Interfaces;
using UPS.Common.Exceptions;

namespace UPS.Application.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserService _userService;

    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAsync(new DeleteUserRequest(request.Id), cancellationToken);

        if (result?.Success == false)
            throw new AppException(result.Errors.First().Message);
    }
}