using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using UPS.Application.Core.Contracts.Requests;
using UPS.Application.Core.Contracts.Responses;
using UPS.Application.Core.Interfaces;
using UPS.Common.Serializers;
using UPS.Common.Utility;

namespace UPS.Application.Core.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<UserService> _logger;
    private readonly IJsonSerializer _jsonSerializer;

    public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger,
        IJsonSerializer jsonSerializer)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<Result<UserPagedListResponse>> GetAllUsersAsync(GetUserPagedListRequest request, int page,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("UpsApiService");

            var response = await client
                .GetAsync($"users?page={page}&{Utils.GenerateQuerySearchFromRequest(request)}", cancellationToken)
                .ConfigureAwait(false);

            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogInformation("Response: {ResponseBody}", responseBody);

            if (!response.IsSuccessStatusCode)
                return Result<UserPagedListResponse>.Fail(new List<ApiError>
                    {new() {Message = "Something went wrong"}});


            return Result<UserPagedListResponse>.Ok(new UserPagedListResponse
            {
                Paging = Utils.GetPaging(response),
                Users = _jsonSerializer.Deserialize<List<UserResponse>>(responseBody)
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while sending request to {RelativeUrl}", "users");

            return Result<UserPagedListResponse>.Fail(new List<ApiError> {new() {Message = e.Message}});
        }
    }

    public async Task<Result<UserResponse?>> GetUserAsync(long id, CancellationToken cancellationToken)
        => await GetAsync<UserResponse?>($"users/{id}", cancellationToken);

    public async Task<Result<UserResponse>?> CreateUserAsync(CreateUserRequest request,
        CancellationToken cancellationToken)
        => await SendRequest<UserResponse>("users", _jsonSerializer.Serialize(request), HttpMethod.Post,
            cancellationToken);


    public async Task<Result<UserResponse>?> UpdateUserRequestAsync(UpdateUserRequest request,
        CancellationToken cancellationToken)
        => await SendRequest<UserResponse>($"users/{request.Id}", _jsonSerializer.Serialize(request), HttpMethod.Put,
            cancellationToken);

    public async Task<Result<object?>?> DeleteAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        => await SendRequest<object?>($"users/{request.Id}", string.Empty, HttpMethod.Delete, cancellationToken);


    private async Task<Result<T>?> SendRequest<T>(string relativeUrl, string data, HttpMethod method,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("UpsApiService");

            var request = new HttpRequestMessage(method, relativeUrl)
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogInformation("Response: {ResponseBody}", responseBody);

            if (response.IsSuccessStatusCode)
            {
                var result = _jsonSerializer.Deserialize<T>(responseBody);

                return Result<T>.Ok(result!);
            }


            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var error = _jsonSerializer.Deserialize<List<ApiError>>(responseBody);

                _logger.LogError("Error while sending request to {RelativeUrl}. Error: {Error!}", relativeUrl,
                    error.First());

                return Result<T>.Fail(error);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<T>.Fail(new List<ApiError> {new() {Message = "Not found"}});


            return Result<T>.Fail(new List<ApiError> {new() {Message = "Something went wrong"}});
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while sending request to {RelativeUrl}", relativeUrl);
            return default;
        }
    }


    private async Task<Result<T>> GetAsync<T>(string relativeUrl, CancellationToken cancellationToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("UpsApiService");

            var response = await client.GetAsync(relativeUrl, cancellationToken).ConfigureAwait(false);

            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            _logger.LogInformation("Response: {ResponseBody}", responseBody);


            if (response.IsSuccessStatusCode)
            {
                var result = _jsonSerializer.Deserialize<T>(responseBody);

                return Result<T>.Ok(result!);
            }


            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errors = _jsonSerializer.Deserialize<List<ApiError>>(responseBody);

                _logger.LogError("Error while sending request to {RelativeUrl}. Error: {Error!}", relativeUrl,
                    errors);

                return Result<T>.Fail(errors);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<T>.Fail(new List<ApiError> {new() {Message = "Not found"}});


            return Result<T>.Fail(new List<ApiError> {new() {Message = "Something went wrong"}});
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while sending request to {RelativeUrl}", relativeUrl);
            return Result<T>.Fail(new List<ApiError> {new() {Message = e.Message}});
        }
    }
}