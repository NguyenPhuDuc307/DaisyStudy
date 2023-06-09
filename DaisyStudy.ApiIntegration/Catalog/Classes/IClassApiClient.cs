﻿using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ApiIntegration.Catalog.Classes;

public interface IClassApiClient
{
    Task<ApiResult<PagedResult<ClassViewModel>>> GetManageClassPaging(GetManageClassPagingRequest request);
    Task<ApiResult<PagedResult<ClassViewModel>>> GetPublicClassPaging(GetManageClassPagingRequest request);
    Task<bool> CreateClass(ClassCreateRequest request);
    Task<string> UploadImage(ClassImageUpdateRequest request);
    Task<ApiResult<ClassViewModel>> GetById(int ClassID);
}