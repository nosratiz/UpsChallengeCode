namespace UPS.Common.Exceptions;

public class AppException : Exception
{
    public AppException() : base("Request failed due to validation errors")
    {
    }

    public AppException(string? message) : base(message)
    {
    }
}