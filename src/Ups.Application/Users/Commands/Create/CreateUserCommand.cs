using MediatR;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;

namespace UPS.Application.Users.Commands.Create;

public sealed record CreateUserCommand : IRequest<Result<UserResponse>>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Status { get; set; } = null!;
}