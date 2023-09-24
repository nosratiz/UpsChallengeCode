namespace UPS.Application.Core.Contracts.Requests;

public sealed record CreateUserRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Status { get; set; } = null!;
}