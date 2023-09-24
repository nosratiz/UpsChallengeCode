namespace UPS.Application.Core.Contracts.Responses;

public sealed record UserResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Status { get; set; }
}