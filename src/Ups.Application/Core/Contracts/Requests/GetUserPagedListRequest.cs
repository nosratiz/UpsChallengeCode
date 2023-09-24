namespace UPS.Application.Core.Contracts.Requests;

public sealed record GetUserPagedListRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Status { get; set; }
};