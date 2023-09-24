using MediatR;

namespace UPS.Application.Users.Commands.Delete;

public sealed record DeleteUserCommand(long Id) : IRequest;