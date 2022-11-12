using System.Net.Http.Headers;
using DaisyStudy.ApiIntegration.Common;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Comments;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.ApiIntegration.Catalog.Comments;
public class CommentApiClient : BaseApiClient, ICommentApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public CommentApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CreateComment(CommentCreateRequest request)
    {
        var requestContent = new MultipartFormDataContent();

        if (request.CommentImages != null)
        {
            byte[] data;

            foreach (var item in request.CommentImages)
            {
                using (var br = new BinaryReader(item.OpenReadStream()))
                {
                    data = br.ReadBytes((int)item.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "commentImages", item.FileName);
            }  
        }

        requestContent.Add(new StringContent(request.UserName.ToString()), "userName");
        requestContent.Add(new StringContent(request.NotificationID.ToString()), "notificationID");
        if (request.Content != null)
        {
            requestContent.Add(new StringContent(request.Content.ToString()), "Content");
        }
        
        var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        var response = await client.PostAsync($"/api/comments", requestContent);

         if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<ApiResult<PagedResult<CommentViewModel>>> GetCommentPaging(GetManageCommentPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<CommentViewModel>>>($"/api/comments/paging?pageIndex=" +
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

    }
}