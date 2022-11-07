using System.Net.Http.Headers;
using DaisyStudy.ApiIntegration.Catalog;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.ClassImages;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.ApiIntegration.Common.Homeworks;
public class HomeworkApiClient : BaseApiClient, IHomeworkApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HomeworkApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResult<bool>> CreateHomework(HomeworkCreateRequest request)
    {
        return await PostAsync<ApiResult<bool>>($"/api/homeworks", request);
    }

    public async Task<ApiResult<PagedResult<HomeworkViewModel>>> GetHomeworkPaging(GetManageHomeworkPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<HomeworkViewModel>>>($"/api/homeworks/paging?pageIndex="+
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            
    }
}