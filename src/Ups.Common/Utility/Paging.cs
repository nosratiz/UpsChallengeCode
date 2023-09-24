namespace UPS.Common.Utility;

public sealed record Paging
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
};