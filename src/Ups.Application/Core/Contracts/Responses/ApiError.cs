namespace UPS.Application.Core.Contracts.Responses;

public class ApiError
{
    public string Field { get; set; } = null!;
    public string Message { get; set; } = null!;
}