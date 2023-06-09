﻿using System.Net.Http.Headers;
using DaisyStudy.ApiIntegration.Common;
using DaisyStudy.Utilities.Constants;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DaisyStudy.ApiIntegration.Catalog.Classes;
public class ClassApiClient : BaseApiClient, IClassApiClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ClassApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CreateClass(ClassCreateRequest request)
    {
        var requestContent = new MultipartFormDataContent();

        if (request.ThumbnailImage != null)
        {
            byte[] data;
            using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
            {
                data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
            }
            ByteArrayContent bytes = new ByteArrayContent(data);
            requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
        }

        requestContent.Add(new StringContent(request.UserName.ToString()), "userName");
        requestContent.Add(new StringContent(request.ClassName.ToString()), "className");
        requestContent.Add(new StringContent(request.Topic.ToString()), "topic");
        requestContent.Add(new StringContent(request.Description.ToString()), "description");
        requestContent.Add(new StringContent(request.ClassRoom.ToString()), "classRoom");
        requestContent.Add(new StringContent(request.Tuition.ToString()), "tuition");

        requestContent.Add(new StringContent(request.SEOClassName.ToString()), "seoClassName");
        requestContent.Add(new StringContent(request.SEODescription.ToString()), "seoDescription");
        requestContent.Add(new StringContent(request.SEOAlias.ToString()), "seoAlias");

        var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        var response = await client.PostAsync("/api/classes/", requestContent);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<ApiResult<PagedResult<ClassViewModel>>> GetManageClassPaging(GetManageClassPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<ClassViewModel>>>("/api/classes/manage/paging?pageIndex="+
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
    }

    public async Task<ApiResult<PagedResult<ClassViewModel>>> GetPublicClassPaging(GetManageClassPagingRequest request)
    {
        return await GetAsync<ApiResult<PagedResult<ClassViewModel>>>("/api/classes/paging?pageIndex="+
            $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
    }

    public async Task<string> UploadImage(ClassImageUpdateRequest request)
    {
        var requestContent = new MultipartFormDataContent();

        if (request.ThumbnailImage != null)
        {
            byte[] data;
            using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
            {
                data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
            }
            ByteArrayContent bytes = new ByteArrayContent(data);
            requestContent.Add(bytes, "imageFile", request.ThumbnailImage.FileName);
        }

        var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
        var response = await client.PostAsync("/api/classes/uploadImage", requestContent);
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return body;
        }
        return null;
    }

    public async Task<ApiResult<ClassViewModel>> GetById(int ClassID)
    {
        return await GetAsync<ApiResult<ClassViewModel>>($"/api/classes/{ClassID}");
    }
}