using FluentAssertions;
using MediatR;
using Moq;
using UPS.Application.Core;
using UPS.Application.Core.Contracts.Responses;
using UPS.Application.Users.Commands.Create;
using UPS.Application.Users.Commands.Delete;
using UPS.Application.Users.Commands.Update;
using UPS.Application.Users.Queries;
using UPS.Common.Exceptions;
using Xunit;

namespace Ups.UnitTest.Users;

public class UserTest
{
    private readonly Mock<IMediator> _mediator;

    public UserTest()
    {
        _mediator = new Mock<IMediator>();
    }

    [Theory]
    [ClassData(typeof(RegisterInvalidTestData))]
    public async Task WhenCreateUser_WithInvalidData_ShouldReturnException(string name,string email,string gender,string status)
    {
        _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AppException());

        var createUser = UserFakeData.CreateUser(name, email, gender, status);

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        
        Func<Task> act = async () => await baseConfiguration.Send(createUser);

        await act.Should().ThrowAsync<AppException>();
    }


    [Fact]
    public async Task WhenCreateUser_WithValidData_ShouldCreateUserSuccessfully()
    {
        _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserResponse>.Ok(new UserResponse()));

        var createUser = UserFakeData.CreateUser("nima", "nima@gmail.com", "male", "active");

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        var result = await baseConfiguration.Send(createUser);

        result.Should().NotBeNull().And.BeOfType<Result<UserResponse>>();
    }



    [Fact]
    public async Task WhenGetUser_WithValidData_ShouldGetUserSuccessfully()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(),
                           It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserResponse>.Ok(new UserResponse()));

        var getUser = new GetUserQuery(1);

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        var result = await baseConfiguration.Send(getUser);

        result.Should().NotBeNull().And.BeOfType<Result<UserResponse>>();
    }


    [Fact]
    public async Task WhenGetUser_WithInvalidData_ShouldReturnException()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserResponse>.Fail(new List<ApiError> { new() { Message = "Not found" } }));

        var getUser = new GetUserQuery(-1);

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        var result = await baseConfiguration.Send(getUser);

        result.Should().BeOfType<Result<UserResponse>>().Which.Errors.Should().NotBeEmpty();
    }


    [Fact]
    public async Task WhenGetAllUser_WithValidData_ShouldGetAllUserSuccessfully()
    {
        _mediator.Setup(x => x.Send(It.IsAny<GetUserPagedListQuery>(),
                           It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserPagedListResponse>.Ok(new UserPagedListResponse()));

        var getAllUser = new GetUserPagedListQuery();

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        var result = await baseConfiguration.Send(getAllUser);

        result.Should().NotBeNull().And.BeOfType<Result<UserPagedListResponse>>().Which.Data.Should().NotBeNull();
    }



    [Theory]
    [ClassData(typeof(RegisterValidData))]
    public async Task WhenEditUser_WithValid_ShouldUpdateSuccessfully(int id, string name, string email, string gender, string status)
    {
        _mediator.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserResponse>.Ok(new UserResponse{Email = email,Gender = gender,Id = id,Name = name,Status = status}));


        var updateUser = UserFakeData.UpdateUser(id,name, email, gender, status);

        var baseConfiguration = new BaseConfiguration(_mediator.Object);

        var result = await baseConfiguration.Send(updateUser);

        result.Should().NotBeNull().And.BeOfType<Result<UserResponse>>().Which.Data.Email!.Contains(email);

    }





}