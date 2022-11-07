using DaisyStudy.ViewModels.Catalog.Homeworks;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.Homeworks;

public interface IHomeworkService
{
    Task<int> Create(HomeworkCreateRequest request);
    Task<int> Update(HomeworkUpdateRequest request);
    Task<int> Delete(int ID);
    Task<HomeworkViewModel> GetById(int ID);
    Task<ApiResult<PagedResult<HomeworkViewModel>>> GetAllPaging(GetManageHomeworkPagingRequest request);
}