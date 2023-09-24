using System.Threading;
using System.Threading.Tasks;
using UPS.Application.Core.Contracts.Requests;
using UPS.Application.Core.Contracts.Responses;

namespace UPS.Application.Core.Interfaces;

public interface IUserService
{
    Task<Result<UserPagedListResponse>> GetAllUsersAsync(GetUserPagedListRequest request,int page, CancellationToken cancellationToken);
    
    Task<Result<UserResponse?>> GetUserAsync(long id, CancellationToken cancellationToken);

    Task<Result<UserResponse>?> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
    
    Task<Result<UserResponse>?> UpdateUserRequestAsync(UpdateUserRequest request, CancellationToken cancellationToken);
    
    Task<Result<object?>?> DeleteAsync(DeleteUserRequest request, CancellationToken cancellationToken);
}