using FluentValidation;
using MediatR;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;
using UPS.Application.Core.Interfaces;
using UPS.Application.Users.Mapping;
using UPS.Common.Exceptions;

namespace UPS.Application.Users.Commands.Create;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserResponse>?>
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IUserService userService, IValidator<CreateUserCommand> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task<Result<UserResponse>?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new AppException(validationResult.Errors.First().ErrorMessage);

        var userRequest = request.FromCommand();

       return await _userService.CreateUserAsync(userRequest, cancellationToken);
    }
}