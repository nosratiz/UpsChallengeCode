using FluentValidation;

namespace UPS.Application.Users.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        
        RuleFor(dto => dto.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);


        RuleFor(dto => dto.Gender).Must(dto => dto.ToLower() is "male" or "female")
            .WithMessage("invalid Gender Type");
        
        RuleFor(dto => dto.Status).Must(dto => dto.ToLower() is "active" or "inactive")
            .WithMessage("invalid Status Type");
    }
}