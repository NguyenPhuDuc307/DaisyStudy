using System.Net.Http.Headers;
using System.Text;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;
using Newtonsoft.Json;

namespace DaisyStudy.AdminApp.Service;

public class UserApiClient : IUserApiClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    public UserApiClient(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    public async Task<string> Authenticate(LoginRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var client = _clientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        var response = await client.PostAsync("/api/users/authenticate", httpContent);
        var token = await response.Content.ReadAsStringAsync();
        return token;
    }

    public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
    {
        var client = _clientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["BaseAddress"]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.BearerToken);
        var response = await client.GetAsync($"/api/Users/paging?PageIndex=" +
            $"{request.PageIndex}&PageSize={request.PageSize}&KeyWord={request.Keyword}");
        var body = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<PagedResult<UserViewModel>>(body);
        return users;
    }
}