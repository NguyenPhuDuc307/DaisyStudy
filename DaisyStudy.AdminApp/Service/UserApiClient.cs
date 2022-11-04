using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.AdminApp.Service;

public class UserApiClient : BaseApiClient, IUserApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResult<string>> Authenticate(LoginRequest request)
    {
        return await PostAsync<ApiResult<string>>($"/api/users/authenticate", request);
    }

    public async Task<ApiResult<UserViewModel>> GetById(Guid id)
    {
        return await GetAsync<ApiResult<UserViewModel>>($"/api/users/{id}");
    }

    public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<UserViewModel>>>($"/api/users/paging?pageIndex=" +
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
    }

    public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
    {

        return await PostAsync<ApiResult<bool>>($"/api/users", request);
    }

    public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
    {
        return await PutAsync<ApiResult<bool>>($"/api/users/{id}", request);
    }

    public async Task<ApiResult<bool>> Delete(Guid id)
    {
        return await DeleteAsync<ApiResult<bool>>($"/api/users/{id}");
    }

    public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
    {
        return await PutAsync<ApiResult<bool>>($"/api/users/{id}/roles", request);
    }
}