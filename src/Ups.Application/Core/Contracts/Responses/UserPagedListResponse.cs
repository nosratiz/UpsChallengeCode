using System.Collections.Generic;
using UPS.Common.Utility;

namespace UPS.Application.Core.Contracts.Responses;

public sealed class UserPagedListResponse
{
   public Paging Paging { get; set; } = null!;
    public List<UserResponse>? Users { get; set; } = null!;
}