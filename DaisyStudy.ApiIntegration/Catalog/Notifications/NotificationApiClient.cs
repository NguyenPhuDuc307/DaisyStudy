using System.Net.Http.Headers;
using DaisyStudy.ApiIntegration.Common;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Notifications;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.ApiIntegration.Catalog.Notifications;
public class NotificationApiClient : BaseApiClient, INotificationApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public NotificationApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CreateNotification(NotificationCreateRequest request)
    {
        var requestContent = new MultipartFormDataContent();

        if (request.ThumbnailImages != null)
        {
            byte[] data;

            foreach (var item in request.ThumbnailImages)
            {
                using (var br = new BinaryReader(item.OpenReadStream()))
                {
                    data = br.ReadBytes((int)item.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImages", item.FileName);
            }  
        }

        requestContent.Add(new StringContent(request.ClassID.ToString()), "ClassID");
        requestContent.Add(new StringContent(request.Title.ToString()), "Title");
        requestContent.Add(new StringContent(request.Content.ToString()), "Content");
        
        var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        var response = await client.PostAsync($"/api/notifications", requestContent);

         if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<ApiResult<PagedResult<NotificationViewModel>>> GetNotificationPaging(GetManageNotificationPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<NotificationViewModel>>>($"/api/notifications/paging?pageIndex=" +
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

    }
}