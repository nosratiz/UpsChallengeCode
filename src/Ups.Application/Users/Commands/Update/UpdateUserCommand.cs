using MediatR;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;

namespace UPS.Application.Users.Commands.Update;

public sealed record UpdateUserCommand : IRequest<Result<UserResponse>>
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Status { get; set; } = null!;
}