using DaisyStudy.ViewModels.Catalog.Submissions;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Submissions;
public interface ISubmissionService
{
    Task<ApiResult<bool>> Create(SubmissionCreateRequest request);
    Task<ApiResult<bool>> Update(SubmissionUpdateRequest request);
    Task<ApiResult<bool>> Delete(int HomeworkID, string UserName);
    Task<SubmissionViewModel> GetById(int HomeworkID, string UserName);
    Task<PagedResult<SubmissionViewModel>> GetAllPaging(GetManageSubmissionPagingRequest request);
}