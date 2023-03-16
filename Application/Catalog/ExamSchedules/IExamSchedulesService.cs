using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.ExamSchedules;
using DaisyStudy.Models.Common;

namespace DaisyStudy.Application.Catalog.ExamSchedules;

public interface IExamScheduleService
{
    Task<int> Create(ExamSchedulesCreateRequest request);
    Task<int> Update(ExamSchedulesUpdateRequest request);
    Task<int> Delete(int ExamScheduleID);
    Task<ExamSchedulesViewModel> GetById(int ExamScheduleID);
    Task<PagedResult<ExamSchedulesViewModel>> GetAllPaging(GetManageExamSchedulesPagingRequest request);
    Task<PagedResult<ExamSchedulesViewModel>> GetAllMyExamSchedulesPaging(GetManageExamSchedulesPagingRequest request);
    Task<PagedResult<ExamSchedulesViewModel>> GetAllMyExamAdminSchedulesPaging(GetManageExamSchedulesPagingRequest request);
    Task<List<ExamSchedulesViewModel>> GetAllMyExamAdminSchedules(string UserId);
    Task<List<ExamSchedulesViewModel>> GetAllMyExamSchedules(string UserName);
}