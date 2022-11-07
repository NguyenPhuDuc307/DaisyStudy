using DaisyStudy.ViewModels.Catalog.Homeworks;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.ApiIntegration.Common.Homeworks;

public interface IHomeworkApiClient
{
    Task<ApiResult<PagedResult<HomeworkViewModel>>> GetHomeworkPaging(GetManageHomeworkPagingRequest request);
    Task<ApiResult<bool>> CreateHomework(HomeworkCreateRequest request);
}