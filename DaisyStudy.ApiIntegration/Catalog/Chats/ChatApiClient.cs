using System.Net.Http.Headers;
using DaisyStudy.ApiIntegration.Common;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Chats;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.ApiIntegration.Catalog.Chats;
public class ChatApiClient : BaseApiClient, IChatApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ChatApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CreateChat(ChatCreateRequest request)
    {
        var requestContent = new MultipartFormDataContent();

        if (request.ChatImages != null)
        {
            byte[] data;

            foreach (var item in request.ChatImages)
            {
                using (var br = new BinaryReader(item.OpenReadStream()))
                {
                    data = br.ReadBytes((int)item.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "chatImages", item.FileName);
            }  
        }

        requestContent.Add(new StringContent(request.UserName.ToString()), "userName");
        requestContent.Add(new StringContent(request.ClassID.ToString()), "classID");
        requestContent.Add(new StringContent(request.Content.ToString()), "Content");
        
        var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        var response = await client.PostAsync($"/api/chats", requestContent);

         if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<ApiResult<PagedResult<ChatViewModel>>> GetChatPaging(GetManageChatPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<ChatViewModel>>>($"/api/chats/paging?pageIndex=" +
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

    }
}