using DaisyStudy.Data.Entities;
using DaisyStudy.ViewModels.Catalog.Classes;
using DaisyStudy.ViewModels.Common;
using DaisyStudy.ViewModels.System.Users;

namespace DaisyStudy.Application.Catalog.Classes
{
    public interface IClassService
    {
        Task<int> Create(ClassCreateRequest request);
        Task<string> UploadImage(ClassImageUpdateRequest request);
        Task<int> Update(ClassUpdateRequest request);
        Task<int> Delete(int ID);
        Task<ApiResult<ClassViewModel>> GetById(int ID);
        Task<bool> UpdateTuition(int ID, decimal tuition);
        Task<bool> UpdateStatus(int ID, Status status);
        Task<bool> UpdateIsPublic(int ID, IsPublic isPublic);
        Task AddViewCount(int ID);
        Task<ApiResult<PagedResult<ClassViewModel>>> GetManageAllClassPaging(GetManageClassPagingRequest request);
        Task<ApiResult<PagedResult<ClassViewModel>>> GetPublicAllClassPaging(GetManageClassPagingRequest request);
        Task<PagedResult<ClassDetailViewModel>> GetAllStudentByClassIDPaging(GetAllStudentInClassPagingRequest request);
        Task<PagedResult<ClassViewModel>> GetAll(GetPublicClassPagingRequest request);
        Task<int> UpdateImage(int classID, ClassImageUpdateRequest request);
        Task<bool> ChangeClassID(int ID);
        Task<bool> AddStudent(string ClassID, string UserName);
    }
}