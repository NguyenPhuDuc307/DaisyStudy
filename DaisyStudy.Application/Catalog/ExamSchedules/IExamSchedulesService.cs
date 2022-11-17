using DaisyStudy.ViewModels.Catalog.ExamSchedules;
using DaisyStudy.ViewModels.Catalog.Homeworks;
using DaisyStudy.ViewModels.Common;

namespace DaisyStudy.Application.Catalog.ExamSchedules;

public interface  IExamSchedulesService
{
    Task<int> Create(ExamSchedulesCreateRequest request);
    Task<int> Update(ExamSchedulesUpdateRequest request);
    Task<int> Delete(int ExamScheduleID);
    Task<ExamSchedulesViewModel> GetById(int ExamScheduleID);
    Task<ApiResult<PagedResult<ExamSchedulesViewModel>>> GetAllPaging(GetManageExamSchedulesPagingRequest request);
}