using System.Collections;
using UPS.Application.Users.Commands.Create;

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