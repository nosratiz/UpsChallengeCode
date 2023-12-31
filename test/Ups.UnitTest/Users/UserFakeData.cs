﻿using System.Collections;
using UPS.Application.Users.Commands.Create;
using UPS.Application.Users.Commands.Update;

namespace Ups.UnitTest.Users;

public class UserFakeData
{
    public static CreateUserCommand CreateUser(string name, string email, string gender, string status)
    {
        return new CreateUserCommand
        {
            Email = email,
            Gender = gender,
            Name = name,
            Status = status
        };
    }


    public static UpdateUserCommand UpdateUser(long Id, string name, string email, string gender, string status)
    {
        return new UpdateUserCommand
        {
            Id = Id,
            Email = email,
            Gender = gender,
            Name = name,
            Status = status

        };
    }
}

public class RegisterInvalidTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {"nima","nima@gmail.com","mil","active" };
        yield return new object[] { "nima", "nima@gmail.com", "male", "active2" };
        yield return new object[] { "nima", "nima.com", "male", "active" };
        yield return new object[] { "n", "nima@gmail.com", "male", "active" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}



public class RegisterValidData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, "nima", "nima@gmail.com", "male", "active" };
        yield return new object[] {2, "nima", "nima2@gmail.com", "male", "active" };
        yield return new object[] {3, "nima", "nima3@gmail.com", "male", "active" };
        yield return new object[] { 4,"nima", "nima4@gmail.com", "male", "active" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}