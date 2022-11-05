using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Catalog.ClassImages;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.AdminApp.Service;

public interface IClassApiClient
{
    Task<PagedResult<ClassViewModel>> GetClassPaging(GetManageClassPagingRequest request);
    Task<bool> CreateClass(ClassCreateRequest request);
    Task<string> UploadImage(ClassImageCreateRequest request);
}