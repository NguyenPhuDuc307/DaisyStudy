using DaisyStudy.ViewModels.Catalog.Comments;
using DaisyStudy.ViewModels.Catalog.StudentExams;
using DaisyStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyStudy.Application.Catalog.StudentExams
{
    public interface IStudentExamService
    {
        Task<int> Create(StudentExamsCreateRequest request);
        Task<int> Update(StudentExamsUpdateRequest request);
        Task<int> Delete(int StudentExamID);
        Task<ApiResult<StudentExamsViewModel>> GetById(int ExamScheduleID);
        Task<ApiResult<PagedResult<StudentExamsViewModel>>> GetAllPaging(GetManageStudentExamPagingRequest request);
    }
}