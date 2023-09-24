using UPS.Application.Core.Contracts.Responses;

namespace UPS.Application.Core;

public sealed class Result<T>
{
    public bool Success { get; set; }

    public string Message { get; set; }
    public T Data { get; set; }
    
    public List<ApiError> Errors { get; set; } = new();


    public static Result<T> Ok(T data)
    {
        return new Result<T>
        {
            Success = true,
            Data = data
        };
    }

    public static Result<T> Fail(List<ApiError> errors)
    {
        return new Result<T>
        {
            Success = false,
            Message = "Validation failed",
            Errors = errors
        };
    }
}


public sealed class Result
{
    public bool Success { get; set; }

    public string Message { get; set; }
    
    public static Result Ok()
    {
        return new Result
        {
            Success = true,
        };
    }

    public static Result Fail(string message)
    {
        return new Result
        {
            Success = false,
            Message = message
        };
    }
}