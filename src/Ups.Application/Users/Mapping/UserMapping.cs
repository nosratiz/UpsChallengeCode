using UPS.Application.Core.Contracts.Requests;
using UPS.Application.Users.Commands.Create;
using UPS.Application.Users.Commands.Update;
using UPS.Application.Users.Queries;

namespace UPS.Application.Users.Mapping;

public static class UserMapping
{
    public static CreateUserRequest FromCommand(this CreateUserCommand command)
    {
        return new CreateUserRequest
        {
            Email = command.Email,
            Gender = command.Gender,
            Name = command.Name,
            Status = command.Status
        };
    }


    public static UpdateUserRequest FromCommand(this UpdateUserCommand command)
    {
        return new UpdateUserRequest
        {
            Id = command.Id,
            Email = command.Email,
            Gender = command.Gender,
            Name = command.Name,
            Status = command.Status
        };
    }


    public static GetUserPagedListRequest FromCommand(this GetUserPagedListQuery query)
    {
        return new GetUserPagedListRequest
        {
            Name = query.Name,
            Email = query.Email,
            Gender = query.Gender,
            Status = query.Status
        };
    }
}